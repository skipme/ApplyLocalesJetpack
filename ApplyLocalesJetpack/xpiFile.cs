using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ApplyLocalesJetpack
{
    public class xpiFile
    {
        public string takeXML(string xpiPath)
        {
            using (ZipFile zipFile = new ZipFile(xpiPath))
            {
                int i = zipFile.FindEntry("install.rdf", false);
                ZipEntry ze = zipFile[i];
                using (Stream s = zipFile.GetInputStream(ze))
                {
                    byte[] buff = new byte[(int)ze.Size];
                    s.Read(buff, 0, buff.Length);
                    return Encoding.UTF8.GetString(buff);
                }
            }
        }
        public void setXML(string xpiPath, string rdf)
        {
            ZipFile zipFile = new ZipFile(xpiPath);

            zipFile.BeginUpdate();
            CustomStaticDataSource sds = new CustomStaticDataSource();
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(rdf));
            ms.Position = 0;
            sds.SetStream(ms);

            // If an entry of the same name already exists, it will be overwritten; otherwise added.
            zipFile.Add(sds, "install.rdf");

            // Both CommitUpdate and Close must be called.
            zipFile.CommitUpdate();
            zipFile.Close();
        }
    }
    public class CustomStaticDataSource : IStaticDataSource
    {
        private Stream _stream;
        // Implement method from IStaticDataSource
        public Stream GetSource()
        {
            return _stream;
        }

        // Call this to provide the memorystream
        public void SetStream(Stream inputStream)
        {
            _stream = inputStream;
            _stream.Position = 0;
        }
    }
}

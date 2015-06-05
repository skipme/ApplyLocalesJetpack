using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace ApplyLocalesJetpack
{
    class Program
    {
        static void Main(string[] args)
        {
            string defPath = @"some.xpi";
            string defJson = "[{l: 'en-US', name: 'test', description: 'testD'}]";
            string defPJson = "{updateUrl: 'update.rdf', homepageUrl: 'http://', versions: ['26.0', '39.0'], updateLink: '.xpi', updIURL: ''}";

            if (args.Length == 2)
            {
                defPath = args[0];
                defJson = File.ReadAllText(args[1], Encoding.Unicode);
            }
            else if (args.Length == 3)
            {
                defPath = args[0];
                defJson = File.ReadAllText(args[1], Encoding.Unicode);
                defPJson = File.ReadAllText(args[2], Encoding.Unicode);
            }
            else
            {
                Console.WriteLine("not consistent arguments (2: addon.xpi locales.json, 3: ...params.json");
                return;
            }

            List<locales.lObject> o = locales.get(defJson);
            xpiFile f = new xpiFile();
            installrdf rdf = new installrdf(f.takeXML(defPath));
            for (int i = 0; i < o.Count; i++)
            {
                rdf.pushloc(o[i]);
            }
            uparams.pObject p = uparams.get(defPJson);
            if (!string.IsNullOrWhiteSpace(p.updateUrl))
            {
                rdf.setMinMaxVer(p.versions[0], p.versions[1]);
                rdf.pushUpdateUrl(p.updateUrl, p.homepageUrl);
            }

            f.setXML(defPath, rdf.Take());
            Console.WriteLine("writed {0} locales", o.Count);

            if (!string.IsNullOrWhiteSpace(p.updateUrl))
            {
                string sha256 = GetChecksum(defPath);
                string jid = rdf.getId();
                string ver = rdf.getVer();

                string urfFName = new Uri(p.updateUrl).Segments.Last();
                if (string.IsNullOrWhiteSpace(urfFName) || urfFName == "/")
                {
                    urfFName = "update.rdf";
                }
                string to = Path.Combine(Path.GetDirectoryName(defPath), urfFName);
                updaterdf urdf = new updaterdf();
                urdf.set(jid, ver, p.versions[0], p.versions[1],
                    p.updateLink, sha256, p.updIURL);
                File.WriteAllText(to, urdf.Take(), Encoding.UTF8);
            }
        }
        private static string GetChecksum(string file)
        {
            using (FileStream stream = File.OpenRead(file))
            {
                var sha = new SHA256Managed();
                byte[] checksum = sha.ComputeHash(stream);
                return BitConverter.ToString(checksum).Replace("-", String.Empty);
            }
        }
    }
}

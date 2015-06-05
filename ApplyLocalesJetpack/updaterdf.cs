using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace ApplyLocalesJetpack
{
    public class updaterdf
    {
        string mockup = @"
<RDF:RDF xmlns:RDF=""http://www.w3.org/1999/02/22-rdf-syntax-ns#""
         xmlns:em=""http://www.mozilla.org/2004/em-rdf#"">

  <RDF:Description about=""..."">
    <em:updates>
      <RDF:Seq>
        <RDF:li>
          <RDF:Description>
            <em:version></em:version>
            <em:targetApplication>
              <RDF:Description>
                <em:id>{ec8030f7-c20a-464f-9b0e-13a3a9e97384}</em:id>
                <em:minVersion></em:minVersion>
                <em:maxVersion></em:maxVersion>
                <em:updateLink></em:updateLink>
                <em:updateHash></em:updateHash>
                <em:updateInfoURL></em:updateInfoURL>
              </RDF:Description>
            </em:targetApplication>
          </RDF:Description>
        </RDF:li>
      </RDF:Seq>
    </em:updates>
  </RDF:Description>
</RDF:RDF>";
        XmlDocument doc;
        public updaterdf(string rdf = null)
        {
            doc = new XmlDocument();
            doc.LoadXml(rdf ?? mockup);
        }
        public string Take()
        {
            using (MemoryStream ms = new MemoryStream())
            {
                StreamWriter sw = new StreamWriter(ms, Encoding.UTF8);
                doc.Save(sw);
                return Encoding.UTF8.GetString(ms.GetBuffer(), 3, (int)ms.Length - 3);// 3 byted BOM for UTF-8
            }
        }
        public void set(string jid, string jver, string minver, string maxver, string updatelink, string sha256, string updateInfoURL)
        {
            XmlNode n = getNode(new[] { "RDF:RDF", "RDF:Description" });
            n.Attributes["about"].InnerText = "urn:mozilla:extension:" + jid;

            n = getNode(new[] { "RDF:RDF", "RDF:Description", "em:updates", "RDF:Seq", "RDF:li", "RDF:Description", 
                "em:version" });
            n.InnerText = jver;

            n = getNode(new[] { "RDF:RDF", "RDF:Description", "em:updates", "RDF:Seq", "RDF:li", "RDF:Description", 
                "em:targetApplication","RDF:Description", "em:minVersion" });
            n.InnerText = minver;

            n = getNode(new[] { "RDF:RDF", "RDF:Description", "em:updates", "RDF:Seq", "RDF:li", "RDF:Description", 
                "em:targetApplication","RDF:Description", "em:maxVersion" });
            n.InnerText = maxver;

            n = getNode(new[] { "RDF:RDF", "RDF:Description", "em:updates", "RDF:Seq", "RDF:li", "RDF:Description", 
                "em:targetApplication","RDF:Description", "em:updateLink" });
            n.InnerText = updatelink;

            n = getNode(new[] { "RDF:RDF", "RDF:Description", "em:updates", "RDF:Seq", "RDF:li", "RDF:Description", 
                "em:targetApplication","RDF:Description", "em:updateHash" });
            if (n != null)
                n.InnerText = "sha256:" + sha256.ToLower();
            else Console.WriteLine("hashSum skipped in update.rdf");

            n = getNode(new[] { "RDF:RDF", "RDF:Description", "em:updates", "RDF:Seq", "RDF:li", "RDF:Description", 
                "em:targetApplication","RDF:Description", "em:updateInfoURL" });
            if (n != null)
                n.InnerText = updateInfoURL;
            else Console.WriteLine("updateInfoURL skipped in update.rdf");
        }

        public XmlNode getNode(string[] name)
        {
            XmlNode cn = doc;
            for (int i = 0; i < name.Length; i++)
            {
                cn = cn[name[i]];
            }
            return cn;
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace ApplyLocalesJetpack
{
    public class installrdf
    {
        XmlDocument doc;
        public installrdf(string rdf)
        {
            doc = new XmlDocument();
            doc.LoadXml(rdf);
        }
        public string Take()
        {
            using (MemoryStream ms = new MemoryStream())
            {
                StreamWriter sw = new StreamWriter(ms, Encoding.UTF8);
                doc.Save(sw);
                return Encoding.UTF8.GetString(ms.GetBuffer(), 3, (int)ms.Length-3);// 3 byted BOM for UTF-8
            }
        }
        public string getId()
        {
            XmlNode n = getNode(new[] { "RDF", "Description", "em:id" });
            return n.InnerText;
        }
        public string getVer()
        {
            XmlNode n = getNode(new[] { "RDF", "Description", "em:version" });
            return n.InnerText;
        }
        public void pushloc(locales.lObject obj)
        {
            XmlNode n = getNode(new[] { "RDF", "Description" });
            n.AppendChild(CreateNode(obj));
        }
        public void pushUpdateUrl(string updateUrl, string homepageUrl = null)
        {
            //<em:homepageURL>https://www..../</em:homepageURL>
            //<em:updateURL>https://...firefox.rdf</em:updateURL>
            XmlNode n = getNode(new[] { "RDF", "Description" });

            XmlElement elem = doc.CreateElement("em:updateURL", "http://www.mozilla.org/2004/em-rdf#");
            elem.InnerText = updateUrl;

            n.AppendChild(elem);

            if (homepageUrl != null)
            {
                XmlElement elemH = doc.CreateElement("em:homepageURL", "http://www.mozilla.org/2004/em-rdf#");
                elemH.InnerText = homepageUrl;

                n.AppendChild(elemH);
            }
        }
        public void setMinMaxVer(string minVer, string maxVer)
        {
            XmlNode n = getNode(new[] { "RDF", "Description", "em:targetApplication", "Description", "em:minVersion" });
            n.InnerText = minVer;

            n = getNode(new[] { "RDF", "Description", "em:targetApplication", "Description", "em:maxVersion" });
            n.InnerText = maxVer;
        }
        public XmlNode CreateNode(locales.lObject lobj)
        {
            XmlElement elem = doc.CreateElement("em:localized", "http://www.mozilla.org/2004/em-rdf#");
            XmlElement elem1 = doc.CreateElement("Description");
            XmlElement elem2 = doc.CreateElement("em:locale", "http://www.mozilla.org/2004/em-rdf#"); elem2.InnerText = lobj.l;
            XmlElement elem3 = doc.CreateElement("em:name", "http://www.mozilla.org/2004/em-rdf#"); elem3.InnerText = lobj.name;
            XmlElement elem4 = doc.CreateElement("em:description", "http://www.mozilla.org/2004/em-rdf#"); elem4.InnerText = lobj.description;

            elem1.AppendChild(elem2);
            elem1.AppendChild(elem3);
            elem1.AppendChild(elem4);

            elem.AppendChild(elem1);
            return elem;
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

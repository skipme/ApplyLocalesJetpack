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
            MemoryStream ms = new MemoryStream();
            doc.Save(ms);
            return Encoding.UTF8.GetString(ms.GetBuffer(), 0, (int)ms.Length);
        }
        public void pushloc(locales.lObject obj)
        {
            XmlNode n = getNode(new[] { "RDF", "Description" });
            n.AppendChild(CreateNode(obj));
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

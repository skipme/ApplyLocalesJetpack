using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ApplyLocalesJetpack
{
    class Program
    {
        static void Main(string[] args)
        {
            string defPath = @"some.xpi";
            string defJson = "[{l: 'en-US', name: 'test', description: 'testD'}]";

            if (args.Length == 2)
            {
                defPath = args[0];
                defJson = File.ReadAllText(args[1], Encoding.Unicode);
            }
            else
            {
                Console.WriteLine("not enough arguments");
                return;
            }

            List<locales.lObject> o = locales.getLocales(defJson);
            xpiFile f = new xpiFile();
            installrdf rdf = new installrdf(f.takeXML(defPath));
            for (int i = 0; i < o.Count; i++)
            {
                rdf.pushloc(o[i]);
            }
            
            f.setXML(defPath, rdf.Take());
            Console.WriteLine("writed {0} locales", o.Count);
        }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplyLocalesJetpack
{
    public class locales
    {
        /*
         * 
         *  [{l: "", name: "", description: ""}]
         * 
         * 
         * 
         * 
         */ 
        public class lObject
        {
            public string l { get; set; }
            public string name { get; set; }
            public string description { get; set; }
        }
        public static List<lObject> getLocales(string json)
        {
            List<lObject> m = JsonConvert.DeserializeObject<List<lObject>>(json);
            return m;
        }
    }
}

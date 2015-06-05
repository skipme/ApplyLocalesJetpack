using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplyLocalesJetpack
{
    public class uparams
    {
        /*
         * 
         *  "{updateUrl: '', homepageUrl: '', versions: ['26.0', '38.5'], updateLink: ''}";
         * 
         * 
         * 
         * 
         */ 
        public class pObject
        {
            public string updateUrl { get; set; }
            public string updIURL { get; set; }
            public string homepageUrl { get; set; }
            public string[] versions { get; set; }
            public string updateLink { get; set; }
        }
        public static pObject get(string json)
        {
            pObject m = JsonConvert.DeserializeObject<pObject>(json);
            return m;
        }
    }
}

using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Map.Models {
    public class infotabs {
        virtual public int id { get; set; }
        virtual public string content { get; set; }
        virtual public string title { get; set; }
        virtual public int sort { get; set; }
        virtual public infotabs_templates template { get; set; }
        [JsonIgnore]
        virtual public IList<place> places { get; set; }
    }

}

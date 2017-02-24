using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Collections.Generic;

namespace Map.Models {
    public class infotabs_templates : publish_base {
        virtual public int id { get; set; }
        virtual public string name { get; set; }
        virtual public string alias { get; set; }
        virtual public string content { get; set; }
        virtual public bool process { get; set; }
        virtual public IList<infotabs> infotabs { get; set; }
    }
}

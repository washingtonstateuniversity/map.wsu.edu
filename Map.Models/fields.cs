using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;

namespace Map.Models
{

    public class fields 
    {
        public fields() { }
        virtual public int id { get; set; } 
        virtual public field_types type { get; set; }
        virtual public string value { get; set; }
        virtual public int owner { get; set; }
    }
}


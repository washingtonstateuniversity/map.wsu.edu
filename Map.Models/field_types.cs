using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;

namespace Map.Models
{
    public class field_types
    {
        virtual public int id { get; set; }
        virtual public string name { get; set; }
        virtual public string alias { get; set; }
        virtual public string attr { get; set; }
        virtual public string model { get; set; }
        virtual public int set { get; set; }
        virtual public bool is_public { get; set; }
        virtual public IList<users> authors { get; set; }
        virtual public IList<user_groups> access_levels { get; set; }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;

namespace Map.Models
{
    public class style_option_types
    {
        virtual public int id { get; set; }
        virtual public string name { get; set; }
        virtual public IList<geometrics_types> style_type { get; set; }
    }
}
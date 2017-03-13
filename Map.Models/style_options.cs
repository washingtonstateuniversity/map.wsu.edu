using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;

namespace Map.Models
{
    public class style_options
    {
        virtual public int id { get; set; }
        virtual public style_option_types type { get; set; }
        virtual public geometric_events user_event { get; set; }
        virtual public zoom_levels zoom { get; set; }
        virtual public string value { get; set; }
    }
}
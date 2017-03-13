using System;
using System.Collections.Generic;

namespace Map.Models
{
    public class media_format
    {
        virtual public int id { get; set; }
        virtual public string name { get; set; }
        virtual public string attr { get; set; }
        virtual public IList<media_types> media_types { get; set; }
    }
}

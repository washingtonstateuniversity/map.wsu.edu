using System;
using System.Collections.Generic;
using System.IO;

namespace Map.Models {
    public class place_names {
        virtual public int id { get; set; }
        virtual public int place_id { get; set; }
        virtual public String name { get; set; }
        virtual public place_name_types label { get; set; }
    }
}
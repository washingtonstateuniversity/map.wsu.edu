using System;
using System.Collections.Generic;
using System.IO;

namespace Map.Models {
    
    public class google_types {
        virtual public int id { get; set; }
        virtual public string name { get; set; }
        virtual public IList<place_types> place_type { get; set; }
    }

}


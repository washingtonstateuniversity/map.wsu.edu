using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Map.Models {
    public class place_types {
        virtual public int id { get; set; }
        virtual public string name { get; set; }
        virtual public string friendly { get; set; }
        [JsonIgnore]
        virtual public IList<place> places { get; set; }
        virtual public IList<google_types> google_type { get; set; }
    }
}
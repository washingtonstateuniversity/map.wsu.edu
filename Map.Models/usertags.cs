using System.Collections.Generic;
using Newtonsoft.Json;

namespace Map.Models {

    public class usertags {
        virtual public int id { get; set; }
        virtual public string name { get; set; }
        virtual public string attr { get; set; }
        [JsonIgnore]
        virtual public IList<place> Places { get; set; }
    }

}

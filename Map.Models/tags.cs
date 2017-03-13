using System.Collections.Generic;
using Newtonsoft.Json;

namespace Map.Models {
    public class tags {
		[JsonIgnore]
		virtual public int id { get; set; }
        virtual public string name { get; set; }
		[JsonIgnore]
		virtual public string attr { get; set; }
        [JsonIgnore]
        virtual public IList<place> places { get; set; }
    }
}
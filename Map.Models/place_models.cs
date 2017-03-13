using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Map.Models {
	public class place_models {
		virtual public int id { get; set; }
		virtual public string name { get; set; }
		virtual public string attr { get; set; }
		virtual public IList<field_types> field_types { get; set; }
        [JsonIgnore]
        virtual public IList<place> Places { get; set; }
    }
}
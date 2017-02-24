using System.Collections.Generic;
using Newtonsoft.Json;

namespace Map.Models
{

    public class categories
    {
        virtual public int id { get; set; }
        virtual public string name { get; set; }
        virtual public int level { get; set; }
        virtual public int position { get; set; }
        virtual public string url { get; set; }
        virtual public bool asLink { get; set; }
        virtual public bool active { get; set; }
        virtual public string friendly_name { get; set; }
        virtual public categories Parent { get; set; }
        [JsonIgnore]
        virtual public IList<place> Places { get; set; }
		[JsonIgnore]
		virtual public IList<categories> Children { get; set; }
	}
    
}

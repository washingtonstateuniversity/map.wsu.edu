using System.Collections.Generic;
using Newtonsoft.Json;

namespace Map.Models
{

    public class campus 
    {
        virtual public int id { get; set; }
        virtual public bool gameDayTourOn { get; set; }
        virtual public string city { get; set; }
        virtual public string name { get; set; }
        virtual public string url { get; set; }
        virtual public string state { get; set; }
        virtual public string state_abbrev { get; set; }
        virtual public int zipcode { get; set; }
        virtual public decimal latitude { get; set; }
        virtual public decimal longitude { get; set; }
        [JsonIgnore]
        virtual public IList<place> Places { get; set; }
    }

}

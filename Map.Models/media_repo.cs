using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Map.Models
{
    public class media_repo 
    {
        virtual public int id { get; set; }
        virtual public string credit { get; set; }
        virtual public string caption { get; set; }
        virtual public DateTime? created { get; set; }
        virtual public DateTime? updated { get; set; }
        virtual public String file_name { get; set; }
        virtual public String ext { get; set; }
        virtual public String path { get; set; }
        virtual public String orientation { get; set; }
        virtual public media_types type { get; set; }
        virtual public IList<place_media> place_media { get; set; }
        [JsonIgnore]
        virtual public IList<place_media> Ordering { get; set; }
        [JsonIgnore]
        virtual public IList<place_media> Places { get; set; }
        virtual public IList<advertisement> Advertisements { get; set; }
        virtual public IList<fields> field { get; set; }
    }

}

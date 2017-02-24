using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace Map.Models {
    public class geometrics : publish_base {
        virtual public int id { get; set; }
        virtual public byte[] boundary { get; set; }
        virtual public string name { get; set; }
        virtual public string encoded { get; set; }
        virtual public string staticMap { get; set; }
        virtual public geometrics_types default_type { get; set; }
        [JsonIgnore]
        virtual public geometrics parent { get; set; }
        [JsonIgnore]
        virtual public IList<geometrics> children { get; set; }
        virtual public media_repo media { get; set; }
        [JsonIgnore]
        virtual public IList<tags> tags { get; set; }
        [JsonIgnore]
        virtual public IList<geometrics_types> geometric_types { get; set; }
        [JsonIgnore]
        virtual public IList<place> Places { get; set; }
        [JsonIgnore]
        virtual public IList<map_views> views { get; set; }
        [JsonIgnore]
        virtual public IList<fields> field { get; set; }
        virtual public IList<styles> style { get; set; }
        [JsonIgnore]
        virtual public IList<media_repo> Images { get; set; }
        [JsonIgnore]
        virtual public IList<users> Authors { get; set; }
    }

}


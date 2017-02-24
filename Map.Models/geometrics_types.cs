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
    public class geometrics_types {
        virtual public int id { get; set; }
        virtual public string name { get; set; }
        virtual public string attr { get; set; }
        [JsonIgnore]
        virtual public IList<geometrics> Geometrics { get; set; }
        [JsonIgnore]
        virtual public IList<style_option_types> ops { get; set; }
    }
}


using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using Newtonsoft.Json;

namespace Map.Models
{
    public class field_types
    {
		[JsonIgnore]
		virtual public int id { get; set; }
        virtual public string name { get; set; }
		[JsonIgnore]
		virtual public string alias { get; set; }
		[JsonIgnore]
		virtual public string attr { get; set; }
		[JsonIgnore]
		virtual public string model { get; set; }
		[JsonIgnore]
		virtual public place_models set { get; set; }
		[JsonIgnore]
		virtual public bool is_public { get; set; }
		[JsonIgnore]
		virtual public IList<fields> fields { get; set; }
		[JsonIgnore]
		virtual public IList<users> authors { get; set; }
		[JsonIgnore]
		virtual public IList<user_groups> access_levels { get; set; }
    }
}

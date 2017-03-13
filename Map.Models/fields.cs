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
    public class fields
    {
		[JsonIgnore]
        virtual public int id { get; set; }
		virtual public field_types type { get; set; }
        virtual public string value { get; set; }
		[JsonIgnore]
		virtual public int owner { get; set; }
    }
}

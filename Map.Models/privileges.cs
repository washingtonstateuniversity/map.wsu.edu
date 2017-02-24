using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using Newtonsoft.Json;

namespace Map.Models {

    public class privileges {
        virtual public int id { get; set; }
        virtual public String name { get; set; }
        virtual public String alias { get; set; }
        virtual public Boolean editable { get; set; }
        virtual public String description { get; set; }
        virtual public IList<user_groups> access_levels { get; set; }
    }

}

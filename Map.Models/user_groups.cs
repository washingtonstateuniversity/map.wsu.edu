using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using Newtonsoft.Json;

namespace Map.Models {

    public class user_groups {
        virtual public int id { get; set; }
        [JsonIgnore]
        virtual public user_groups parent { get; set; }
        virtual public String name { get; set; }
        virtual public String alias { get; set; }
        virtual public Boolean default_group { get; set; }
        virtual public Boolean isAdmin { get; set; }
        virtual public Boolean allow_signup { get; set; }
        [JsonIgnore]
        virtual public IList<users> users { get; set; }
        [JsonIgnore]
        virtual public IList<user_groups> children { get; set; }
        [JsonIgnore]
        virtual public IList<field_types> field_types { get; set; }
        [JsonIgnore]
        virtual public IList<privileges> privileges { get; set; }
        [JsonIgnore]
        virtual public IList<colleges> colleges { get; set; }
        [JsonIgnore]
        virtual public IList<campus> campus { get; set; }
        [JsonIgnore]
        virtual public IList<programs> programs { get; set; }
        [JsonIgnore]
        virtual public IList<schools> schools { get; set; }
        [JsonIgnore]
        virtual public IList<categories> categories { get; set; }
    }
    
}

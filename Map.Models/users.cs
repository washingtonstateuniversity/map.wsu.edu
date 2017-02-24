using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using Newtonsoft.Json;

namespace Map.Models {

    public class users  {
        virtual public int id { get; set; }
        virtual public String nid { get; set; }
        virtual public user_groups groups { get; set; }
        virtual public user_settings settings { get; set; }
        virtual public string name { get; set; }
        virtual public string email { get; set; }
        virtual public string phone { get; set; }
        virtual public bool active { get; set; }
        virtual public bool logedin { get; set; }
        private DateTime? lastActive;
        virtual public DateTime? LastActive {
            get { return lastActive; }
            set {
                if ((value >= (DateTime)SqlDateTime.MinValue) && (value <= (DateTime)SqlDateTime.MaxValue))
                {
                    lastActive = value;
                }
            }
        }
        [JsonIgnore]
        virtual public IList<media_repo> media { get; set; }
        [JsonIgnore]
        virtual public IList<place> Places { get; set; }
        [JsonIgnore]
        virtual public IList<field_types> field_types { get; set; }
        [JsonIgnore]
        virtual public IList<geometrics> geometric { get; set; }
        [JsonIgnore]
        virtual public IList<map_views> views { get; set; }
        [JsonIgnore]
        virtual public IList<place_types> place_types { get; set; }
        [JsonIgnore]
        virtual public IList<colleges> colleges { get; set; }
        [JsonIgnore]
        virtual public IList<campus> campus { get; set; }
        [JsonIgnore]
        virtual public IList<programs> programs { get; set; }
        [JsonIgnore]
        virtual public IList<categories> categories { get; set; }
        /*[JsonIgnore]
        virtual public IList<place> editing { get; set; } */
    }
  
}

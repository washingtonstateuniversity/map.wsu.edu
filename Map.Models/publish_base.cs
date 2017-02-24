using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using Newtonsoft.Json;

namespace Map.Models {
    public class publish_base {
        private DateTime? Creation_Date;
        virtual public DateTime? creation_date
        {
            get { return Creation_Date; }
            set
            {
                //DateTime bla = DateTime.MinValue;
                if ((value >= (DateTime)SqlDateTime.MinValue) && (value <= (DateTime)SqlDateTime.MaxValue))
                {
                    // bla is a valid sql datetime
                    Creation_Date = value;
                }
            }
        }
        private DateTime? Updated_Date;
        virtual public DateTime? updated_date
        {
            get { return Updated_Date; }
            set
            {
                if ((value >= (DateTime)SqlDateTime.MinValue) && (value <= (DateTime)SqlDateTime.MaxValue))
                {
                    // bla is a valid sql datetime
                    Updated_Date = value;
                }
            }
        }
        private DateTime? Publish_Time;
        virtual public DateTime? publish_time {
            get { return Publish_Time; }
            set {
                //DateTime bla = DateTime.MinValue;
                if ((value >= (DateTime)SqlDateTime.MinValue) && (value <= (DateTime)SqlDateTime.MaxValue)) {
                    // bla is a valid sql datetime
                    Publish_Time = value;
                }
            }
        }

        virtual public Boolean tmp { get; set; }
        virtual public Boolean outputError { get; set; }
        virtual public Boolean isPublic { get; set; }
        virtual public Boolean needs_update { get; set; }
        [JsonIgnore]
        virtual public users editing { get; set; }
        [JsonIgnore]
        virtual public users owner { get; set; }
        virtual public status status { get; set; }
    }
}

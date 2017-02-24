using System;
using System.Data.SqlTypes;

namespace Map.Models {
    public class logs  {
        virtual public int id { get; set; }
        virtual public string entry { get; set; }
        virtual public string code { get; set; }
        virtual public int obj_id { get; set; }
        virtual public string action { get; set; }
        virtual public string controller { get; set; }
        virtual public string nid { get; set; }
        virtual public string ip { get; set; }
        private DateTime? _logdate;
        virtual public DateTime? date {
            get { return _logdate; }
            set {
                if ((value >= (DateTime)SqlDateTime.MinValue) && (value <= (DateTime)SqlDateTime.MaxValue)) {
                    // bla is a valid sql datetime
                    _logdate = value;
                }
            }
        }
    }
}

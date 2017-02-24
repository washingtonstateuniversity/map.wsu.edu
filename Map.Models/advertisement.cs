using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Collections.Generic;
using System.Data.SqlTypes;

namespace Map.Models
{
    public class advertisement
    {
        virtual public int id { get; set; }
        virtual public int Clicked { get; set; }
        virtual public String Url { get; set; }
        virtual public IList<media_repo> Images { get; set; }
        virtual public IList<tags> Tags { get; set; }
        virtual public IList<fields> field { get; set; }
        virtual public int Views { get; set; }
        virtual public String HtmlText { get; set; }
        virtual public String Name { get; set; }
        virtual public place_types place_types { get; set; }
        virtual public String limitAds { get; set; }
        virtual public int maxClicks { get; set; }
        virtual public int maxImpressions { get; set; }
        private DateTime? Expiration;
        virtual public DateTime? expiration
        {
            get { return Expiration; }
            set
            {
                //DateTime bla = DateTime.MinValue;
                if ((value >= (DateTime)SqlDateTime.MinValue) && (value <= (DateTime)SqlDateTime.MaxValue))
                {
                    // bla is a valid sql datetime
                    Expiration = value;

                }
            }
        }
        private DateTime? StartDate;
        virtual public DateTime? startdate
        {
            get { return StartDate; }
            set
            {
                //DateTime bla = DateTime.MinValue;
                if ((value >= (DateTime)SqlDateTime.MinValue) && (value <= (DateTime)SqlDateTime.MaxValue))
                {
                    // bla is a valid sql datetime
                    StartDate = value;

                }
            }
        }
        virtual public bool isExpired()
        {
            if ((!expiration.HasValue || expiration.HasValue && expiration.Value.CompareTo(DateTime.Now) >= 0) && (maxClicks == 0 || Clicked < maxClicks) && (maxImpressions == 0 || Views < maxImpressions))
                return false;
            return true;
        }
        public static Comparison<advertisement> OrderComparison = delegate(advertisement p1, advertisement p2)
        {
            DateTime t1datetime, t2datetime;
            if (!DateTime.TryParse(p1.expiration.ToString(), out t1datetime))
                t1datetime = DateTime.MaxValue;
            if (!DateTime.TryParse(p2.expiration.ToString(), out t2datetime))
                t2datetime = DateTime.MaxValue;

            if (p1.expiration == p2.expiration && p1.Clicked == p2.Clicked){
                        return p1.Views.CompareTo(p2.Views);
            }else if (p1.expiration == p2.expiration){
                return p1.Clicked.CompareTo(p2.Clicked);
            }
            return t1datetime.CompareTo(t2datetime);
        };
    }
}

using System;
using System.Collections.Generic;

namespace Map.Models
{
    public class media_types 
    {
        public static int ad = 2;
        public static int place = 1;
        virtual public int id { get; set; }
        virtual public string name { get; set; }
        virtual public string attr { get; set; }
        virtual public media_format format { get; set; }
        virtual public IList<media_repo> media_typed { get; set; }
    }

}

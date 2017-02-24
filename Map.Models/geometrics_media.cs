using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;

namespace Map.Models {
      
    public class geometrics_media {
        virtual public int Id { get; set; }
        virtual public geometrics geometric { get; set; }
        virtual public media_repo Media { get; set; }
        virtual public int geometric_order { get; set; }
    }
    
}


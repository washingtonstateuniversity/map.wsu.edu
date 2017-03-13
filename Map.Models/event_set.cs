using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;

namespace Map.Models
{
    public class events_set {
        virtual public int id { get; set; }
        virtual public styles style { get; set; }
        virtual public zoom_levels zoom { get; set; }
        virtual public IList<geometric_events> events { get; set; }
    }
}
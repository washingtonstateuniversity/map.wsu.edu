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
      
    public class geometric_events  {
        virtual public int id { get; set; }
        virtual public string name { get; set; }
        virtual public string friendly_name { get; set; }
        virtual public IList<style_options> options { get; set; }
    }
}


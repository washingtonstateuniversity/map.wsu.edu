using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Collections.Generic;

namespace Map.Models
{
    
    public class person_types 
    {
        virtual public int Id { get; set; }
        virtual public string name { get; set; }
        virtual public bool Deleted { get; set; }
        virtual public string attr { get; set; }
    }

}

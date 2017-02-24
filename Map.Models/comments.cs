using System;
using System.Data;
using System.Configuration;
using System.Web;

namespace Map.Models
{
    public class comments 
    {
        virtual public int id { get; set; }
        virtual public string comment { get; set; }
        virtual public string censored { get; set; }
        virtual public bool published { get; set; }
        virtual public bool Flagged { get; set; }
        virtual public int FlagNumber { get; set; }
        virtual public bool adminRead { get; set; }
        virtual public DateTime CreateTime { get; set; }
        virtual public DateTime UpdateTime { get; set; }
        virtual public bool Deleted { get; set; }
        virtual public string Nid { get; set; }
        virtual public string commentorName { get; set; }
        virtual public string Email { get; set; }
        virtual public place place { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.IO;

namespace Map.Models {
    public class place_comments {
        virtual public int id { get; set; }
        virtual public string Comments { get; set; }
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
using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Collections.Generic;

namespace Map.Models
{
    public class person 
    {
        virtual public int Id { get; set; }
        virtual public string Name { get; set; }
        virtual public string Email { get; set; }
        virtual public string Phone { get; set; }
        virtual public string Position { get; set; }
        virtual public bool Deleted { get; set; }
        virtual public bool BreakingNews { get; set; }
        virtual public bool Newsletter { get; set; }
        virtual public user_groups AccessLevelStatus { get; set; }
        virtual public String Nid { get; set; }
        virtual public person_types PersonType { get; set; }
    }
}

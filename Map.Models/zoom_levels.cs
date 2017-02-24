using System.Collections.Generic;

namespace Map.Models
{
    public class zoom_levels 
    {
        virtual public int id { get; set; }
        virtual public int start { get; set; }
        virtual public int end { get; set; }
        virtual public IList<geometric_events> events { get; set; }
     }
}

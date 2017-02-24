using FluentNHibernate.Automapping;
using FluentNHibernate.Mapping;
using NHibernate.Spatial.Type;
using Map.Models;

namespace Map.Data
{
    public class zoomlevelmap : ClassMap<zoom_levels>
    {
        public zoomlevelmap()
        {
            Id(x => x.id, "zoom_id");
            Map(x => x.start, "zoom_start");
            Map(x => x.end, "zoom_end");
            
            HasManyToMany(x => x.events)
               .Table("geometric_events_to_zoom")
               .ParentKeyColumn("zoom_id")
               .ChildKeyColumn("geometric_event_id")
               .Inverse()
               .NotFound.Ignore();

        }
    }
}

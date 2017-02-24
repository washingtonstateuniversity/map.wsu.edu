using FluentNHibernate.Automapping;
using FluentNHibernate.Mapping;
using Map.Models;

namespace Map.Data
{
    public class geometric_eventsmap : ClassMap<geometric_events>
    {
        public geometric_eventsmap()
        {
            Id(x => x.id, "geometric_event_id");
            Map(x => x.friendly_name);
            Map(x => x.name);

            HasManyToMany(x => x.options)
                .Table("geometric_events_to_style_options")
                .ParentKeyColumn("geometric_event_id")
                .ChildKeyColumn("style_option_id")
                .NotFound.Ignore();

        }
    }
}

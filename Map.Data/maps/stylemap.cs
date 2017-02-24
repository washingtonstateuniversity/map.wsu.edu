using FluentNHibernate.Automapping;
using FluentNHibernate.Mapping;
using NHibernate.Spatial.Type;
using Map.Models;

namespace Map.Data
{
    public class stylemap : ClassMap<styles>
    {
        public stylemap()
        {
            Id(x => x.id, "style_id");
            Map(x => x.name);
            HasOne(x => x.type);
            Map(x => x.style_obj);
            HasManyToMany(x => x._option)
                .Table("style_to_style_options")
                .ParentKeyColumn("style_id")
                .ChildKeyColumn("style_option_id")
                .Inverse()
                .NotFound.Ignore();
            HasManyToMany(x => x.g_event)
                .Table("style_to_events_set")
                .ParentKeyColumn("style_id")
                .ChildKeyColumn("events_set_id")
                .Inverse()
                .NotFound.Ignore();
            HasManyToMany(x => x._zoom)
                .Table("style_to_zoom")
                .ParentKeyColumn("style_id")
                .ChildKeyColumn("zoom_id")
                .Inverse()
                .NotFound.Ignore();
        }
    }
}

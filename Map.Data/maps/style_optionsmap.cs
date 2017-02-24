using FluentNHibernate.Automapping;
using FluentNHibernate.Mapping;
using NHibernate.Spatial.Type;
using Map.Models;

namespace Map.Data
{
    public class style_optionsmap : ClassMap<style_options>
    {
        public style_optionsmap()
        {
            Id(x => x.id, "style_option_id");
            HasOne(x => x.type);
            References(x => x.user_event,"event");
            References(x => x.zoom, "zoom");
            Map(x => x.value);
        }
    }
}

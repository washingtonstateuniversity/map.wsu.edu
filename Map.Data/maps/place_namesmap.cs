using FluentNHibernate.Automapping;
using FluentNHibernate.Mapping;
using NHibernate.Spatial.Type;
using Map.Models;

namespace Map.Data
{
    public class place_namesmap : ClassMap<place_names>
    {
        public place_namesmap()
        {
            Id(x => x.id, "name_id");
            Map(x => x.place_id);
            Map(x => x.name);
            References(x => x.label);
        }
    }
}

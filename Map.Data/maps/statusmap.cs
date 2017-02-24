using FluentNHibernate.Automapping;
using FluentNHibernate.Mapping;
using NHibernate.Spatial.Type;
using Map.Models;

namespace Map.Data
{
    public class statusmap : ClassMap<status>
    {
        public statusmap()
        {
            Id(x => x.id, "status_id");
            Map(x => x.name);
        }
    }
}

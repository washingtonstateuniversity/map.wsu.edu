using FluentNHibernate.Automapping;
using FluentNHibernate.Mapping;
using NHibernate.Spatial.Type;
using Map.Models;

namespace Map.Data
{
    public class place_statusmap : ClassMap<place_status>
    {
        public place_statusmap()
        {
            Id(x => x.Id);
            Map(x => x.Title);
        }
    }
}

using FluentNHibernate.Automapping;
using FluentNHibernate.Mapping;
using NHibernate.Spatial.Type;
using Map.Models;

namespace Map.Data
{
    public class place_datamap : ClassMap<place_data>
    {
        public place_datamap()
        {
            Id(x => x.Id);
            Map(x => x.Data);
            Map(x => x.Key, "[Key]");
			References(x => x.Place);
        }
    }
}

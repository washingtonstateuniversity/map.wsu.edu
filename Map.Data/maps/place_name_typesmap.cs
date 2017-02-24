using FluentNHibernate.Automapping;
using FluentNHibernate.Mapping;
using NHibernate.Spatial.Type;
using Map.Models;

namespace Map.Data
{
    public class place_name_typesmap : ClassMap<place_name_types>
    {
        public place_name_typesmap()
        {
            Id(x => x.id, "type_id");
            Map(x => x.type);
        }
    }
}

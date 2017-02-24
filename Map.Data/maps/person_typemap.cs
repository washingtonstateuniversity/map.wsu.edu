using FluentNHibernate.Automapping;
using FluentNHibernate.Mapping;
using NHibernate.Spatial.Type;
using Map.Models;

namespace Map.Data
{
    public class person_typesmap : ClassMap<person_types>
    {
        public person_typesmap()
        {
            Id(x => x.Id);
            Map(x => x.Deleted);
            Map(x => x.attr);
            Map(x => x.name);
        }
    }
}

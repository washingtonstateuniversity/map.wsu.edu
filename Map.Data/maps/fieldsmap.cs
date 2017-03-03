using FluentNHibernate.Automapping;
using FluentNHibernate.Mapping;
using Map.Models;

namespace Map.Data
{
    public class fieldsmap : ClassMap<fields>
    {
        public fieldsmap()
        {
            Id(x => x.id, "field_id");
            References(x => x.type).NotFound.Ignore();
            Map(x => x.owner);
            Map(x => x.value);
        }
    }
}

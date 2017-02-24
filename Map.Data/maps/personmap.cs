using FluentNHibernate.Automapping;
using FluentNHibernate.Mapping;
using NHibernate.Spatial.Type;
using Map.Models;

namespace Map.Data
{
    public class personmap : ClassMap<person>
    {
        public personmap()
        {
            Id(x => x.Id);
            Map(x => x.Name);
            Map(x => x.Email);
            Map(x => x.Phone);
            Map(x => x.Position);
            Map(x => x.Deleted);
            Map(x => x.BreakingNews);
            Map(x => x.Newsletter);
            Map(x => x.Nid);
            HasOne(x => x.AccessLevelStatus);
            References(x => x.PersonType, "personTypeId");
        }
    }
}

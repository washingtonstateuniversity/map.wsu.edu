using FluentNHibernate.Automapping;
using FluentNHibernate.Mapping;
using NHibernate.Spatial.Type;
using Map.Models;

namespace Map.Data
{
    public class infotabsmap : ClassMap<infotabs>
    {
        public infotabsmap()
        {
            Id(x => x.id, "infotab_id");
            Map(x => x.content);
            HasManyToMany(x => x.places)
                .Table("place_to_infotabs")
                .ParentKeyColumn("infotab_id")
                .ChildKeyColumn("place_id")
                .Inverse()
                .NotFound.Ignore();
            Map(x => x.sort);
            HasOne(x => x.template);
            Map(x => x.title);
        }
    }
}

using FluentNHibernate.Automapping;
using FluentNHibernate.Mapping;
using NHibernate.Spatial.Type;
using Map.Models;

namespace Map.Data
{
    public class tagmap : ClassMap<tags>
    {
        public tagmap()
        {
            Id(x => x.id, "tag_id");
            Map(x => x.name);
            Map(x => x.attr);
            HasManyToMany(x => x.places)
                .Table("place_to_tags")
                .ParentKeyColumn("tag_id")
                .ChildKeyColumn("place_id")
                .Inverse()
                .NotFound.Ignore();
        }
    }
}

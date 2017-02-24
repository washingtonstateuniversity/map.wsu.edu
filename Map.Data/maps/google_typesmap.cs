using FluentNHibernate.Automapping;
using FluentNHibernate.Mapping;
using NHibernate.Spatial.Type;
using Map.Models;

namespace Map.Data
{
    public class google_typesmap : ClassMap<google_types>
    {
        public google_typesmap()
        {
            Id(x => x.id, "google_type_id");
            Map(x => x.name);
            HasManyToMany(x => x.place_type)
                .Table("google_types_to_place_types")
                .ParentKeyColumn("google_type_id")
                .ChildKeyColumn("place_type_id")
                .Inverse().NotFound.Ignore();
        }
    }
}

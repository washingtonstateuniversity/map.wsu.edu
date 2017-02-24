using FluentNHibernate.Automapping;
using FluentNHibernate.Mapping;
using NHibernate.Spatial.Type;
using Map.Models;

namespace Map.Data
{
    public class place_typesmap : ClassMap<place_types>
    {
        public place_typesmap()
        {
            Id(x => x.id, "place_type_id");
            Map(x => x.name);
            Map(x => x.friendly, "friendly_name");
            HasManyToMany(x => x.places)
                .Table("place_to_place_types")
                .ParentKeyColumn("place_type_id")
                .ChildKeyColumn("place_id")
                .NotFound.Ignore();
            HasManyToMany(x => x.google_type)
                .Table("google_types_to_place_types")
                .ParentKeyColumn("place_type_id")
                .ChildKeyColumn("google_type_id")
                .NotFound.Ignore();
        }
    }
}

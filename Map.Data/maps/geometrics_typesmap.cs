using FluentNHibernate.Automapping;
using FluentNHibernate.Mapping;
using NHibernate.Spatial.Type;
using Map.Models;

namespace Map.Data
{
    public class geometrics_typesmap : ClassMap<geometrics_types>
    {
        public geometrics_typesmap()
        {
            Id(x => x.id, "geometrics_type_id");
            Map(x => x.attr);
            Map(x => x.name);
            HasManyToMany(x => x.ops)
                .Table("style_option_types_to_geometrics_types")
                .ParentKeyColumn("geometrics_type_id")
                .ChildKeyColumn("style_option_type_id")
                .NotFound.Ignore();
            HasManyToMany(x => x.Geometrics)
                .Table("geometrics_to_types")
                .ParentKeyColumn("geometrics_type_id")
                .ChildKeyColumn("geometric_id")
                .Inverse()
                .NotFound.Ignore();
        }
    }
}

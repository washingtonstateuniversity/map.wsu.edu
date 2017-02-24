using FluentNHibernate.Automapping;
using FluentNHibernate.Mapping;
using NHibernate.Spatial.Type;
using Map.Models;

namespace Map.Data
{
    public class style_option_typesmap : ClassMap<style_option_types>
    {
        public style_option_typesmap()
        {
            Id(x => x.id, "style_option_type_id");
            Map(x => x.name);
            HasManyToMany(x => x.style_type)
                .Table("style_option_types_to_geometrics_types")
                .ParentKeyColumn("style_option_type_id")
                .ChildKeyColumn("geometrics_type_id")
                .Inverse()
                .NotFound.Ignore();
        }
    }
}

using FluentNHibernate.Automapping;
using FluentNHibernate.Mapping;
using NHibernate.Spatial.Type;
using Map.Models;

namespace Map.Data
{
    public class geometricsmap : ClassMap<geometrics>
    {
        public geometricsmap()
        {
            Id(x => x.id, "geometric_id");
			Map(x => x.boundary); //.CustomType(typeof(MsSql2008GeographyType));
            Map(x => x.name);
            Map(x => x.encoded);
            Map(x => x.staticMap);
            HasOne(x => x.default_type);
            HasOne(x => x.parent);
            HasMany(x => x.children)
                .LazyLoad()
                .Cascade.AllDeleteOrphan();
            HasOne(x => x.media);
            HasManyToMany(x => x.tags)
                .Table("geometric_to_tags")
                .ParentKeyColumn("geometric_id")
                .ChildKeyColumn("tag_id")
                .NotFound.Ignore();
            HasManyToMany(x => x.geometric_types)
                .Table("geometrics_to_types")
                .ParentKeyColumn("geometric_id")
                .ChildKeyColumn("geometrics_type_id")
                .NotFound.Ignore();
            HasManyToMany(x => x.Places)
                .Table("place_to_geometrics")
                .ParentKeyColumn("geometric_id")
                .ChildKeyColumn("place_id")
                .Inverse()
                .NotFound.Ignore();
            HasManyToMany(x => x.views)
                .Table("view_to_geometrics")
                .ParentKeyColumn("geometric_id")
                .ChildKeyColumn("view_id")
                .NotFound.Ignore();
            HasManyToMany(x => x.field)
                .Table("geometrics_to_fields")
                .ParentKeyColumn("geometric_id")
                .ChildKeyColumn("field_id")
                .Inverse()
                .NotFound.Ignore();
            HasManyToMany(x => x.style)
                .Table("geometrics_to_styles")
                .ParentKeyColumn("geometric_id")
                .ChildKeyColumn("style_id")
                .NotFound.Ignore();
            HasManyToMany(x => x.Images)
                .Table("geometric_to_media")
                .ParentKeyColumn("geometric_id")
                .ChildKeyColumn("media_id")
                .NotFound.Ignore();
            HasManyToMany(x => x.Authors)
                .Table("authors_to_geometrics")
                .ParentKeyColumn("geometric_id")
                .ChildKeyColumn("author_id")
                .NotFound.Ignore();

            // Publish base
            Map(x => x.creation_date);
            HasOne(x => x.editing);
            Map(x => x.isPublic);
            Map(X => X.needs_update);
            References(x => x.owner, "onwer");
            Map(x => x.outputError);
            Map(x => x.publish_time);
            HasOne(x => x.status);
            Map(x => x.tmp);
            Map(x => x.updated_date);
        }
    }
}

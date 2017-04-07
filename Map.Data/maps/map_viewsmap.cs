using FluentNHibernate.Automapping;
using FluentNHibernate.Mapping;
using NHibernate.Spatial.Type;
using Map.Models;

namespace Map.Data
{
    public class mapviewsmap : ClassMap<map_views>
    {
        public mapviewsmap()
        {
            Id(x => x.id, "view_id");
            Map(x => x.name);
            Map(x => x.alias);
            Map(x => x.key, "idkey");
            Map(x => x.cache, "cache_path");
            Map(x => x.show_global_nav);
            Map(x => x.commentable);
            Map(x => x.sharable);
            Map(x => x.width);
            Map(x => x.height);
            Map(x => x.fit_to_bound);
            Map(x => x.json_style_override);
            Map(x => x.marker_json_style_override);
            Map(x => x.shape_json_style_override);
            HasManyToMany(x => x.Authors)
                .Table("authors_to_view")
                .ParentKeyColumn("view_id")
                .ChildKeyColumn("author_id")
                .NotFound.Ignore();
			Map(x => x.center); //.CustomType(typeof(MsSql2008GeographyType));
			References(x => x.media);
            Map(x => x.staticMap);
            HasMany(x => x.Comments)
                .Table("view_to_comments")
                .KeyColumn("view_id")
                .Cascade.AllDeleteOrphan()
                .Inverse();
            HasMany(x => x.comments_pub)
                .Table("view_to_comments")
                .KeyColumn("view_id")
                .Where("published=1")
                .Cascade.None()
                .Inverse();
            HasManyToMany(x => x.field)
                .Table("view_to_fields")
                .ParentKeyColumn("view_id")
                .ChildKeyColumn("field_id")
                .Inverse()
                .NotFound.Ignore();
            HasManyToMany(x => x.Places)
                .Table("place_to_view")
                .ParentKeyColumn("view_id")
                .ChildKeyColumn("place_id")
                .Inverse()
                .NotFound.Ignore();
            HasManyToMany(x => x.geometrics)
                .Table("view_to_geometrics")
                .ParentKeyColumn("view_id")
                .ChildKeyColumn("geometric_id")
                .Inverse()
                .NotFound.Ignore();
			References(x => x.forced_shapes_style);
			References(x => x.forced_marker_style);
            References(x => x.campus);
            Map(x => x.options_obj, "optionObj");

            // Publish base
            Map(x => x.creation_date);
            References(x => x.editing);
            Map(x => x.isPublic);
            Map(X => X.needs_update);
            References(x => x.owner, "onwer");
            Map(x => x.outputError);
            Map(x => x.publish_time);
            References(x => x.status);
            Map(x => x.tmp);
            Map(x => x.updated_date);
        }
    }
}

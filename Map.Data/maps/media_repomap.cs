using FluentNHibernate.Automapping;
using FluentNHibernate.Mapping;
using NHibernate.Spatial.Type;
using Map.Models;

namespace Map.Data
{
    public class media_repomap : ClassMap<media_repo>
    {
        public media_repomap()
        {
            Id(x => x.id, "media_id");
            Map(x => x.caption);
            Map(x => x.credit);
            Map(x => x.created);
            Map(x => x.updated);
            Map(x => x.file_name);
            Map(x => x.ext);
            Map(x => x.path);
            Map(x => x.orientation);
            References(x => x.type, "media_type_id").LazyLoad(Laziness.NoProxy);
            HasManyToMany(x => x.Ordering)
                .Table("place_media")
                .ParentKeyColumn("media_id")
                .ChildKeyColumn("place_id")
                .NotFound.Ignore();
            HasManyToMany(x => x.Places)
                .Table("place_media")
                .ParentKeyColumn("media_id")
                .ChildKeyColumn("place_id")
                .Inverse()
                .NotFound.Ignore();
            HasManyToMany(x => x.Advertisements)
                .Table("advertisement_to_media")
                .ParentKeyColumn("media_id")
                .ChildKeyColumn("ad_id")
                .Inverse()
                .NotFound.Ignore();
            HasManyToMany(x => x.field)
               .Table("media_to_fields")
               .ParentKeyColumn("media_id")
               .ChildKeyColumn("field_id")
               .Inverse()
               .NotFound.Ignore();
        }
    }
}

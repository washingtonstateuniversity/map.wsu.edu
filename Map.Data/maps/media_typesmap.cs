using FluentNHibernate.Automapping;
using FluentNHibernate.Mapping;
using NHibernate.Spatial.Type;
using Map.Models;

namespace Map.Data
{
    public class media_typesmap : ClassMap<media_types>
    {
        public media_typesmap()
        {
            Id(x => x.id, "media_type_id");
            Map(x => x.name);
            Map(x => x.attr);
            References(x => x.format, "media_format_id").LazyLoad(Laziness.False);
            HasManyToMany(x => x.media_typed)
                .Table("media_to_media_types")
                .ParentKeyColumn("media_type_id")
                .ChildKeyColumn("media_id")
                .Inverse()
                .NotFound.Ignore();
        }
    }
}

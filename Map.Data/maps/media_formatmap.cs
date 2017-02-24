using FluentNHibernate.Automapping;
using FluentNHibernate.Mapping;
using NHibernate.Spatial.Type;
using Map.Models;

namespace Map.Data
{
    public class media_formatmap : ClassMap<media_format>
    {
        public media_formatmap()
        {
            Id(x => x.id, "media_format_id");
            Map(x => x.name);
            Map(x => x.attr);
            HasManyToMany(x => x.media_types)
                .Table("media_types_to_media_format")
                .ParentKeyColumn("media_format_id")
                .ChildKeyColumn("media_type_id")
                .Inverse()
                .NotFound.Ignore();
        }
    }
}

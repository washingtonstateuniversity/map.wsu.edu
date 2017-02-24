using FluentNHibernate.Automapping;
using FluentNHibernate.Mapping;
using NHibernate.Spatial.Type;
using Map.Models;

namespace Map.Data
{
    public class schoolsmap : ClassMap<schools>
    {
        public schoolsmap()
        {
            Id(x => x.id, "school_id");
            Map(x => x.name);
            Map(x => x.url);
            Map(x => x.attr);
            HasManyToMany(x => x.Places)
                .Table("place_to_programs")
                .ParentKeyColumn("school_id")
                .ChildKeyColumn("place_id")
                .Inverse()
                .NotFound.Ignore();
        }
    }
}

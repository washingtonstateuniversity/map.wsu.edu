using FluentNHibernate.Automapping;
using FluentNHibernate.Mapping;
using NHibernate.Spatial.Type;
using Map.Models;

namespace Map.Data
{
    public class programmap : ClassMap<programs>
    {
        public programmap()
        {
            Id(x => x.id, "program_id");
            Map(x => x.name);
            Map(x => x.url);
            Map(x => x.attr);
            HasManyToMany(x => x.Places)
                .Table("place_to_programs")
                .ParentKeyColumn("program_id")
                .ChildKeyColumn("place_id")
                .Inverse()
                .NotFound.Ignore();
        }
    }
}

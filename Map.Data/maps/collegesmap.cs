using FluentNHibernate.Automapping;
using FluentNHibernate.Mapping;
using Map.Models;

namespace Map.Data
{
    public class collegesmap : ClassMap<colleges>
    {
        public collegesmap()
        {
            Id(x => x.id, "college_id");
            Map(x => x.attr);
            Map(x => x.name);
            Map(x => x.url);

            HasManyToMany(x => x.Places)
                .Table("place_to_colleges")
                .ParentKeyColumn("college_id")
                .ChildKeyColumn("place_id")
                .Inverse()
                .NotFound
                .Ignore();
        }
    }
}

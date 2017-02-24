using FluentNHibernate.Automapping;
using FluentNHibernate.Mapping;
using Map.Models;

namespace Map.Data
{
    public class departmentsmap : ClassMap<departments>
    {
        public departmentsmap()
        {
            Id(x => x.id, "department_id");
            Map(x => x.attr);
            Map(x => x.name);
            Map(x => x.url);
            HasManyToMany(x => x.Places)
                .Table("place_to_departments")
                .ParentKeyColumn("department_id")
                .ChildKeyColumn("place_id")
                .Inverse()
                .NotFound.Ignore();
        }
    }
}

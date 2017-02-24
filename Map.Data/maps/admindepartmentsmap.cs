using FluentNHibernate.Automapping;
using FluentNHibernate.Mapping;
using Map.Models;

namespace Map.Data
{
    public class admindepartmentsMap : ClassMap<admindepartments>
    {
        public admindepartmentsMap()
        {
            Id(x => x.id, "admindepartment_id");
            Map(x => x.name);
            Map(x => x.url);
            Map(x => x.attr);
            HasManyToMany(x => x.Places).Table("place_to_admindepartments")
               .ParentKeyColumn("admindepartment_id")
               .ChildKeyColumn("place_id")
               .Inverse()
               .NotFound.Ignore();
        }
    }
}

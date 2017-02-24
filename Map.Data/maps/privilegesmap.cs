using FluentNHibernate.Automapping;
using FluentNHibernate.Mapping;
using NHibernate.Spatial.Type;
using Map.Models;

namespace Map.Data
{
    public class privilegesmap : ClassMap<privileges>
    {
        public privilegesmap()
        {
            Id(x => x.id, "privilege_id");
            Map(x => x.name, "title");
            Map(x => x.alias);
            Map(x => x.editable);
            Map(x => x.description, "discription");
            HasManyToMany(x => x.access_levels)
                .Table("access_levels_to_privilege")
                .ParentKeyColumn("privilege_id")
                .ChildKeyColumn("access_level_id")
                .NotFound.Ignore();
        }
    }
}

using FluentNHibernate.Automapping;
using FluentNHibernate.Mapping;
using NHibernate.Spatial.Type;
using Map.Models;

namespace Map.Data
{
    public class usergroupsmap : ClassMap<user_groups>
    {
        public usergroupsmap()
        {
            Id(x => x.id, "access_level_id");
            References(x => x.parent, "parent");
            Map(x => x.name);
            Map(x => x.alias);
            Map(x => x.default_group).Default("0");
            Map(x => x.isAdmin).Default("0");
            Map(x => x.allow_signup).Default("0");

            HasMany(x => x.users)
                .Inverse()
                .LazyLoad()
                .NotFound.Ignore();
            HasMany(x => x.children)
                .Inverse()
                .LazyLoad()
                .NotFound.Ignore();

            HasManyToMany(x => x.field_types)
                .Table("access_levels_to_field_type")
                .ParentKeyColumn("access_level_id")
                .ChildKeyColumn("field_type_id")
                .LazyLoad()
                .NotFound.Ignore();
            HasManyToMany(x => x.privileges)
                .Table("access_levels_to_privilege")
                .ParentKeyColumn("access_level_id")
                .ChildKeyColumn("privilege_id")
                .LazyLoad()
                .NotFound.Ignore();
            HasManyToMany(x => x.colleges)
                .Table("groups_to_colleges")
                .ParentKeyColumn("access_level_id")
                .ChildKeyColumn("college_id")
                .LazyLoad()
                .NotFound.Ignore();
            HasManyToMany(x => x.campus)
                .Table("groups_to_campus")
                .ParentKeyColumn("access_level_id")
                .ChildKeyColumn("campus_id")
                .LazyLoad()
                .NotFound.Ignore();
            HasManyToMany(x => x.programs)
                .Table("groups_to_programs")
                .ParentKeyColumn("access_level_id")
                .ChildKeyColumn("program_id")
                .LazyLoad()
                .NotFound.Ignore();
            HasManyToMany(x => x.schools)
                .Table("groups_to_schools")
                .ParentKeyColumn("access_level_id")
                .ChildKeyColumn("school_id")
                .LazyLoad()
                .NotFound.Ignore();
            HasManyToMany(x => x.categories)
                .Table("groups_to_categories")
                .ParentKeyColumn("access_level_id")
                .ChildKeyColumn("category_id")
                .LazyLoad()
                .NotFound.Ignore();
        }
    }
}

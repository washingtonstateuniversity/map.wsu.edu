using FluentNHibernate.Automapping;
using FluentNHibernate.Mapping;
using NHibernate.Spatial.Type;
using Map.Models;

namespace Map.Data
{
    public class usersmap : ClassMap<users>
    {
        public usersmap()
        {
            Id(x => x.id, "author_id");
            Map(x => x.nid);
            References(x => x.groups);
            References(x => x.settings);
            Map(x => x.name);
            Map(x => x.email);
            Map(x => x.phone);
            Map(x => x.active);
            Map(x => x.logedin);
            Map(x => x.LastActive);
            HasManyToMany(x => x.media)
                .Table("authors_to_media")
                .ParentKeyColumn("author_id")
                .ChildKeyColumn("media_id")
                .NotFound.Ignore();
            HasManyToMany(x => x.Places)
                .Table("authors_to_place")
                .ParentKeyColumn("author_id")
                .ChildKeyColumn("place_id")
                .Inverse()
                .NotFound.Ignore();
            HasManyToMany(x => x.field_types)
                .Table("authors_to_field_type")
                .ParentKeyColumn("author_id")
                .ChildKeyColumn("field_type_id")
                .NotFound.Ignore();
            HasManyToMany(x => x.geometric)
                .Table("authors_to_geometrics")
                .ParentKeyColumn("author_id")
                .ChildKeyColumn("geometric_id")
                .Inverse()
                .NotFound.Ignore();
            HasManyToMany(x => x.views)
                .Table("authors_to_view")
                .ParentKeyColumn("author_id")
                .ChildKeyColumn("view_id")
                .Inverse()
                .NotFound.Ignore();
            HasManyToMany(x => x.place_types)
                .Table("authors_to_place_type")
                .ParentKeyColumn("author_id")
                .ChildKeyColumn("place_type_id")
                .NotFound.Ignore();
            HasManyToMany(x => x.colleges)
               .Table("authors_to_colleges")
               .ParentKeyColumn("author_id")
               .ChildKeyColumn("college_id")
               .NotFound.Ignore();
            HasManyToMany(x => x.campus)
               .Table("authors_to_campus")
               .ParentKeyColumn("author_id")
               .ChildKeyColumn("campus_id")
               .NotFound.Ignore();
            HasManyToMany(x => x.programs)
               .Table("authors_to_programs")
               .ParentKeyColumn("author_id")
               .ChildKeyColumn("program_id")
               .NotFound.Ignore();
            HasManyToMany(x => x.categories)
               .Table("authors_to_categories")
               .ParentKeyColumn("author_id")
               .ChildKeyColumn("category_id")
               .NotFound.Ignore();
           /* HasMany(x => x.editing)
                .Cascade.AllDeleteOrphan(); */
        }
    }
}

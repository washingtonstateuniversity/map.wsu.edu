using FluentNHibernate.Automapping;
using FluentNHibernate.Mapping;
using NHibernate.Spatial.Type;
using Map.Models;

namespace Map.Data
{
    public class placemap : ClassMap<place>
    {
        public placemap()
        {
            ImportType<Map.Models.searchPlace>();
            Id(x => x.id, "place_id");
            Map(x => x.latitude, "lat");
            Map(x => x.longitude, "lng");
            Map(x => x.infoTitle);
            Map(x => x.prime_name);
            Map(x => x.abbrev_name);
            Map(x => x.summary);
            Map(x => x.details);
            Map(x => x.address);
            Map(x => x.street);
            Map(x => x.city);
            Map(x => x.zip_code);
            Map(x => x.plus_four_code);
            Map(x => x.coordinate);
            Map(x => x.hideTitles);
            Map(x => x.autoAccessibility);
            HasManyToMany(x => x.names)
                .Table("place_to_place_names")
                .ParentKeyColumn("place_id")
                .ChildKeyColumn("name_id")
                .NotFound.Ignore()
                .Cascade.AllDeleteOrphan();
            HasManyToMany(x => x.place_types)
                .Table("place_to_place_types")
                .ParentKeyColumn("place_id")
                .ChildKeyColumn("place_type_id")
                .NotFound.Ignore();
            HasOne(x => x.model);
            HasOne(x => x.media);
            Map(x => x.staticMap);
            Map(x => x.pointImg);
            HasOne(x => x.campus);
            Map(x => x.percentfull);
            HasManyToMany(x => x.departments)
                .Table("place_to_departments")
                .ParentKeyColumn("place_id")
                .ChildKeyColumn("department_id")
                .NotFound.Ignore();
            HasManyToMany(x => x.admindepartments)
                .Table("place_to_admindepartments")
                .ParentKeyColumn("place_id")
                .ChildKeyColumn("admindepartment_id")
                .NotFound.Ignore();
            HasManyToMany(x => x.categories)
                .Table("place_to_categories")
                .ParentKeyColumn("place_id")
                .ChildKeyColumn("category_id")
                .NotFound.Ignore();
            HasManyToMany(x => x.programs)
                .Table("place_to_programs")
                .ParentKeyColumn("place_id")
                .ChildKeyColumn("program_id")
                .NotFound.Ignore();
            HasManyToMany(x => x.views)
                .Table("place_to_view")
                .ParentKeyColumn("place_id")
                .ChildKeyColumn("view_id")
                .NotFound.Ignore();
            HasManyToMany(x => x.fields)
                .Table("place_to_fields")
                .ParentKeyColumn("place_id")
                .ChildKeyColumn("field_id")
                .NotFound.Ignore()
                .Cascade.AllDeleteOrphan();
            HasManyToMany(x => x.tags)
                .Table("place_to_tags")
                .ParentKeyColumn("place_id")
                .ChildKeyColumn("tag_id")
                .NotFound.Ignore();
            HasManyToMany(x => x.usertags)
                .Table("place_to_usertags")
                .ParentKeyColumn("place_id")
                .ChildKeyColumn("usertag_id")
                .NotFound.Ignore();
            HasManyToMany(x => x.Images)
                .Table("place_media")
                .ParentKeyColumn("place_id")
                .ChildKeyColumn("media_id")
                .OrderBy("place_order")
                .Not.LazyLoad()
                .NotFound.Ignore();
            HasMany(x => x.comments)
                .Table("place_to_comments")
                .KeyColumn("place_id")
                .Cascade.AllDeleteOrphan()
                .Inverse();
            HasMany(x=>x.comments_pub)
                .Table("place_to_comments")
                .KeyColumn("place_id")
                .Where("published=1")
                .Cascade.None()
                .Inverse();
            HasManyToMany(x => x.infotabs)
                .Table("place_to_infotabs")
                .ParentKeyColumn("place_id")
                .ChildKeyColumn("infotab_id")
                .NotFound.Ignore();
            HasManyToMany(x => x.authors)
               .Table("authors_to_place")
               .ParentKeyColumn("place_id")
               .ChildKeyColumn("author_id")
               .NotFound.Ignore();
            HasMany(x => x.Placedata);
            HasManyToMany(x => x.geometrics)
                .Table("place_to_geometrics")
                .ParentKeyColumn("place_id")
                .ChildKeyColumn("geometric_id")
                .NotFound.Ignore();


            // Publish base
            Map(x => x.creation_date);
            HasOne(x => x.editing);
            Map(x => x.isPublic);
            Map(X => X.needs_update);
            References(x => x.owner, "onwer");
            Map(x => x.outputError);
            Map(x => x.publish_time);
            HasOne(x => x.status);
            Map(x => x.tmp);
            Map(x => x.updated_date);
        }
    }
}

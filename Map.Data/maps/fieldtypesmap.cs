using FluentNHibernate.Automapping;
using FluentNHibernate.Mapping;
using Map.Models;

namespace Map.Data
{
    public class fieldtypemaps : ClassMap<field_types>
    {
        public fieldtypemaps()
        {
            Id(x => x.id, "field_type_id");
            Map(x => x.alias);
            Map(x => x.attr);
            Map(x => x.is_public);
            Map(x => x.model);
            Map(x => x.name);
			// Map(x => x.set, "fieldset");
			//References(x => x.set, "fieldset");
			//HasOne(x => x.set);
			HasMany(x => x.fields).KeyColumn("type").NotFound.Ignore();

			HasManyToMany(x => x.authors)
                .Table("authors_to_field_type")
                .ParentKeyColumn("field_type_id")
                .ChildKeyColumn("author_id")
                .NotFound.Ignore();

            HasManyToMany(x => x.access_levels)
                .Table("access_levels_to_field_type")
                .ParentKeyColumn("field_type_id")
                .ChildKeyColumn("access_level_id")
                .NotFound.Ignore();
        }
    }
}

using FluentNHibernate.Automapping;
using FluentNHibernate.Mapping;
using Map.Models;

namespace Map.Data
{
    public class categoriesmap : ClassMap<categories>
    {
        public categoriesmap()
        {
            Id(x => x.id, "category_id");
            Map(x => x.level);
            Map(x => x.name);
            References(x => x.Parent, "parent").LazyLoad(Laziness.False);
            HasManyToMany(x => x.Places)
                .Table("place_to_categories")
                .ParentKeyColumn("category_id")
                .ChildKeyColumn("place_id")
                .Inverse()
                .NotFound
                .Ignore();
			Map(x => x.friendly_name);
            Map(x => x.position);
            Map(x => x.url);
			HasMany(x => x.Children).KeyColumn("parent");
        }
    }
}

using FluentNHibernate.Automapping;
using FluentNHibernate.Mapping;
using NHibernate.Spatial.Type;
using Map.Models;

namespace Map.Data
{
    public class place_modelsmap : ClassMap<place_models>
    {
        public place_modelsmap()
        {
            Id(x => x.id, "place_model_id");
            Map(x => x.name);
            Map(x => x.attr);
            HasManyToMany(x => x.Places)
                .Table("place_to_place_models")
                .ParentKeyColumn("place_model_id")
                .ChildKeyColumn("place_id")
                .NotFound.Ignore();
        }
    }
}

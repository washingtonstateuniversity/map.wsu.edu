using FluentNHibernate.Automapping;
using FluentNHibernate.Mapping;
using Map.Models;

namespace Map.Data
{
    public class campusmap : ClassMap<campus>
    {
        public campusmap()
        {
            Id(x => x.id, "campus_id");
            Map(x => x.city);
            Map(x => x.gameDayTourOn);
            Map(x => x.latitude);
            Map(x => x.longitude);
            Map(x => x.name);
            HasMany(x => x.Places)
                .LazyLoad()
                .Inverse()
                .NotFound
                .Ignore();
            Map(x => x.state);
            Map(x => x.state_abbrev);
            Map(x => x.url);
            Map(x => x.zipcode);
        }
    }
}

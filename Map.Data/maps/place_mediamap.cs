using FluentNHibernate.Automapping;
using FluentNHibernate.Mapping;
using NHibernate.Spatial.Type;
using Map.Models;

namespace Map.Data
{
    public class place_mediamap : ClassMap<place_media>
    {
        public place_mediamap()
        {
            Id(x => x.Id);
            References(x => x.Place, "place_id");
            References(x => x.Media, "media_id");
            Map(x => x.place_order);
        }
    }
}

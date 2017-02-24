using FluentNHibernate.Automapping;
using FluentNHibernate.Mapping;
using NHibernate.Spatial.Type;
using Map.Models;

namespace Map.Data
{
    public class geometrics_statusmap : ClassMap<geometrics_media>
    {
        public geometrics_statusmap()
        {
            Id(x => x.Id);
            References(x => x.geometric, "geometric_id");
            References(x => x.Media, "media_id");
            Map(x => x.geometric_order);
        }
    }
}

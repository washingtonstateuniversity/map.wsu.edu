using FluentNHibernate.Automapping;
using FluentNHibernate.Mapping;
using NHibernate.Spatial.Type;
using Map.Models;

namespace Map.Data
{
    public class small_urlmap : ClassMap<small_url>
    {
        public small_urlmap()
        {
            Id(x => x.id, "small_url_id");
            Map(x => x.sm_url, "small_url");
            Map(x => x.or_url, "original");
        }
    }
}

using FluentNHibernate.Automapping;
using FluentNHibernate.Mapping;
using NHibernate.Spatial.Type;
using Map.Models;

namespace Map.Data
{
    public class logsmap : ClassMap<logs>
    {
        public logsmap()
        {
            Id(x => x.id, "log_id");
            Map(x => x.entry);
            Map(x => x.code);
            Map(x => x.obj_id);
            Map(x => x.action);
            Map(x => x.controller);
            Map(x => x.nid);
            Map(x => x.ip);
            Map(x => x.date, "dtOfLog");
        }
    }
}

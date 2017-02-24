using FluentNHibernate.Automapping;
using FluentNHibernate.Mapping;
using NHibernate.Spatial.Type;
using Map.Models;

namespace Map.Data
{
    public class user_settingsmap : ClassMap<user_settings>
    {
        public user_settingsmap()
        {
            Id(x => x.id, "user_settings_id");
            Map(x => x.attr);
        }
    }
}

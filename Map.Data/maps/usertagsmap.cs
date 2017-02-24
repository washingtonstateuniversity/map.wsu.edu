using FluentNHibernate.Automapping;
using FluentNHibernate.Mapping;
using NHibernate.Spatial.Type;
using Map.Models;

namespace Map.Data
{
    public class usertagsmap : ClassMap<usertags>
    {
        public usertagsmap()
        {
            Id(x => x.id, "usertag_id");
            Map(x => x.name);
            Map(x => x.attr);
            
            HasManyToMany(x => x.Places)
               .Table("place_to_usertags")
               .ParentKeyColumn("usertag_id")
               .ChildKeyColumn("place_id")
               .NotFound.Ignore();

        }
    }
}

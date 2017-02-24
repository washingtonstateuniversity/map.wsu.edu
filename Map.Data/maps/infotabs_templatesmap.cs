using FluentNHibernate.Automapping;
using FluentNHibernate.Mapping;
using NHibernate.Spatial.Type;
using Map.Models;

namespace Map.Data
{
    public class infotabstemplatesmap : ClassMap<infotabs_templates>
    {
        public infotabstemplatesmap()
        {
            Id(x => x.id, "template_id");
            Map(x => x.name);
            Map(x => x.alias);
            Map(x => x.content);
            Map(X => X.process);
            HasManyToMany(x => x.infotabs)
                .Table("infotabs_to_infotabs_templates")
                .ParentKeyColumn("template_id")
                .ChildKeyColumn("infotab_id")
                .Inverse()
                .NotFound.Ignore();

            // Publish base
            Map(x => x.creation_date);
            HasOne(x => x.editing);
            Map(x => x.isPublic);
            Map(x => x.needs_update);
            References(x => x.owner, "onwer");
            Map(x => x.outputError);
            Map(x => x.publish_time);
            HasOne(x => x.status);
            Map(x => x.tmp);
            Map(x => x.updated_date);
        }
    }
}

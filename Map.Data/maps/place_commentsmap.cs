using FluentNHibernate.Automapping;
using FluentNHibernate.Mapping;
using NHibernate.Spatial.Type;
using Map.Models;

namespace Map.Data
{
    public class place_commentsmap : ClassMap<place_comments>
    {
        public place_commentsmap()
        {
            Id(x => x.id, "comment_id");
            Map(x => x.Comments);
            Map(x => x.published);
            Map(x => x.Flagged);
            Map(x => x.FlagNumber);
            Map(x => x.adminRead);
            Map(x => x.CreateTime);
            Map(x => x.UpdateTime);
            Map(x => x.Deleted);
            Map(x => x.Nid);
            Map(x => x.commentorName);
            Map(x => x.Email);
            HasOne(x => x.place);
        }
    }
}

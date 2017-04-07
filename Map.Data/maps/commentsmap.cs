using FluentNHibernate.Automapping;
using FluentNHibernate.Mapping;
using Map.Models;

namespace Map.Data
{
    public class commentsmap : ClassMap<comments>
    {
        public commentsmap()
        {
            Id(x => x.id, "comment_id");
            Map(x => x.adminRead);
            Map(x => x.censored);
            Map(x => x.comment);
            Map(x => x.commentorName);
            Map(x => x.CreateTime);
            Map(x => x.Deleted);
            Map(x => x.Email);
            Map(x => x.Flagged);
            Map(x => x.FlagNumber);
            Map(x => x.Nid);
			References(x => x.place);
            Map(x => x.published);
            Map(x => x.UpdateTime);
        }
    }
}

using FluentNHibernate.Automapping;
using FluentNHibernate.Mapping;
using Map.Models;

namespace Map.Data
{
    public class advertisementsMap : ClassMap<advertisement>
    {
        public advertisementsMap()
        {
            Id(x => x.id, "ad_id");
            Map(x => x.Clicked);
            Map(x => x.expiration);
           
            Map(x => x.HtmlText);
            HasManyToMany(x => x.Images)
                .Table("advertisement_to_media")
                .ParentKeyColumn("ad_id")
                .ChildKeyColumn("media_id")
                .NotFound.Ignore();
            Map(x => x.limitAds).Default("0");
            Map(x => x.maxClicks).Default("0");
            Map(x => x.maxImpressions).Default("0");
            Map(x => x.Name);
            HasOne(x => x.place_types);
            Map(x => x.startdate);
            HasManyToMany(x => x.Tags).Table("advertisement_to_tag")
                .ParentKeyColumn("ad_id")
                .ChildKeyColumn("tag_id")
                .NotFound.Ignore();
            Map(x => x.Url);
            Map(x => x.Views).Default("0");
        }
    }
}

﻿using FluentNHibernate.Automapping;
using FluentNHibernate.Mapping;
using NHibernate.Spatial.Type;
using Map.Models;

namespace Map.Data
{
    public class geometrics_mediamap : ClassMap<geometrics_media>
    {
        public geometrics_mediamap()
        {
            Id(x => x.Id);
            Map(x => x.geometric_order);
            HasOne(x => x.geometric);
            HasOne(x => x.Media);            
        }
    }
}

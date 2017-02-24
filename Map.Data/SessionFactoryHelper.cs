using FluentNHibernate.Automapping;
using FluentNHibernate.Mapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;
using FluentNHibernate.Conventions.Helpers;
using Map.Models;
using NHibernate;
using Newtonsoft.Json.Serialization;
using System;

namespace Map.Data
{
    public class SessionFactoryHelper
    {
        public static ISessionFactory CreateSessionFactory()
        {
            var c = Fluently.Configure();
			
            //Replace connectionstring and default schema
            c.Database(MsSqlConfiguration.MsSql2008
                .ConnectionString(con => con.FromConnectionStringWithKey("mapLive"))
                .DefaultSchema("dbo"))
            .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Map.Data.admindepartmentsMap>()
                .Conventions.Setup(con=>
                    {
                        con.Add(ForeignKey.EndsWith(""));
                        con.Add(DefaultLazy.Always());
                    }    
                )
            );                

            return c.BuildSessionFactory();
        }
    }
}

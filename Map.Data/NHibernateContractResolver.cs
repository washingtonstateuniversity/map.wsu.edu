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
using System.Runtime.Serialization;

namespace Map.Data
{
    public class NHibernateContractResolver : DefaultContractResolver
    {
        protected override JsonContract CreateContract(Type objectType)
        {
            if (typeof(NHibernate.Proxy.INHibernateProxy).IsAssignableFrom(objectType))
                return base.CreateContract(objectType.BaseType);
            else
                return base.CreateContract(objectType);
        }
    }
    public class IgnoreSerializableJsonContractResolver : DefaultContractResolver
    {
        protected override JsonContract CreateContract(System.Type objectType)
        {
            /* Behavior in base we're overriding:
            if (typeof(ISerializable).IsAssignableFrom(objectType))
                return CreateISerializableContract(objectType);
            //*/

            if (objectType.IsAutoClass
                  && objectType.Namespace == null
                  && typeof(ISerializable).IsAssignableFrom(objectType))
            {

                return base.CreateObjectContract(objectType);
            }

            return base.CreateContract(objectType);
        }
    }

}

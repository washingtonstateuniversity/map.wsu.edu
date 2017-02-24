using NHibernate;
using NHibernate.Criterion;
using System.Collections.Generic;
using System.Text;
using Map.Models;

namespace Map.Data
{
    public class Repository<T> : IRepository<T>
    {
        private ISession session;

        public Repository()
        {
            var sessionFactory = SessionFactoryHelper.CreateSessionFactory();
            session = sessionFactory.OpenSession();
            session.BeginTransaction();
        }

        public void Save(T obj)
        {
            session.Save(obj);
        }

        public void Update(T obj)
        {
            session.Update(obj);
        }

        public void Delete(T obj)
        {
            session.Delete(obj);
        }

        public T Load<T>(object id)
        { 
            return session.Load<T>(id);
        }

        public T GetReference<T>(object id)
        {
            return session.Get<T>(id);
        }

        public IList<T> GetByHQL<T>(string hql)
        {
            var obj = session.CreateQuery(hql).List<T>();
            return obj;
        }

        public IList<T> GetByProperty<T>(string property, object value)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append(string.Format("FROM {0} a ", typeof(T).FullName));
            hql.Append(string.Format("WHERE a.{0} = ?", property));
            var obj = session.CreateQuery(hql.ToString())
                .SetParameter(0, value)
                .List<T>();

            return obj;
        }

        public T GetFirstByProperty<T>(string property, object value)
        {
            var obj = GetByProperty<T>(property, value);
            if (obj.Count > 0)
                return obj[0];
            else
                return default(T);
        }

        public IList<T> GetAll<T>(int pageIndex, int pageSize)
        {
            ICriteria criteria = session.CreateCriteria(typeof(T));
            criteria.SetFirstResult(pageIndex * pageSize);
            if (pageSize > 0)
            {
                criteria.SetMaxResults(pageSize);
            }
            return criteria.List<T>();
        }

        public IList<T> GetAll<T>()
        {
            return GetAll<T>(0, 0);
        }

        public IList<T> Find<T>(IList<string> strs)
        {
            IList<ICriterion> objs = new List<ICriterion>();
            foreach (string s in strs)
            {
                ICriterion cr1 = Expression.Sql(s);
                objs.Add(cr1);
            }
            ICriteria criteria = session.CreateCriteria(typeof(T));
            foreach (ICriterion rest in objs)
                session.CreateCriteria(typeof(T)).Add(rest);

            criteria.SetFirstResult(0);
            return criteria.List<T>();
        }

        public void Detach(T item)
        {
            session.Evict(item);
        }

        internal void Flush()
        {
            session.Flush();
        }

        public void Commit()
        {
            if (session.Transaction.IsActive)
            {
                session.Transaction.Commit();
            }
        }

        public void Rollback()
        {
            if (session.Transaction.IsActive)
            {
                session.Transaction.Rollback();
                session.Clear();
            }
        }

        public void BeginTransaction()
        {
            Rollback();
            session.BeginTransaction();
        }

        public IList<T> GetAllOrdered<T>(string propertyName, bool ascending)
        {
            Order cr1 = new Order(propertyName, ascending);
            IList<T> objsResult = session.CreateCriteria
                (typeof(T)).AddOrder(cr1).List<T>();
            return objsResult;
        }
    }
}
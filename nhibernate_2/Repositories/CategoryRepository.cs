using Microsoft.AspNetCore.Http;
using NHibernate;
using nhibernate_2.Helpers;
using nhibernate_2.Models;

namespace nhibernate_2.Repositories
{
    public class CategoryRepository
    {
        public void Save(Category category)
        {
            using (NHibernate.ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                session.Save(category);
                transaction.Commit();
            }
        }

        public Category GetById(int id)
        {
            using (NHibernate.ISession session = NHibernateHelper.OpenSession())
            {
                return session.Get<Category>(id);
            }
        }

        public IList<Category> GetAll()
        {
            using (NHibernate.ISession session = NHibernateHelper.OpenSession())
            {
                return session.Query<Category>().ToList();
            }
        }

        public void Update(Category category)
        {
            using (NHibernate.ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                session.Update(category);
                transaction.Commit();
            }
        }

        public void Delete(int id)
        {
            using (NHibernate.ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                var category = session.Get<Category>(id);
                if (category != null)
                {
                    session.Delete(category);
                    transaction.Commit();
                }
            }
        }
    }
}

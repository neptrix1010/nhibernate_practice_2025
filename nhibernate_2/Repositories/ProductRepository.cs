using Microsoft.AspNetCore.Http;
using nhibernate_2.Models;

namespace nhibernate_2.Repositories
{
    public class ProductRepository
    {
        private readonly NHibernate.ISession _session;

        public ProductRepository(NHibernate.ISession session)
        {
            _session = session;
        }

        public void Save(Product product)
        {
            using (var transaction = _session.BeginTransaction())
            {
                // Load the category reference
                if (product.CategoryId > 0)
                {
                    product.Category = _session.Get<Category>(product.CategoryId);
                    if (product.Category == null)
                    {
                        throw new InvalidOperationException($"Category with ID {product.CategoryId} not found");
                    }
                }

                _session.Save(product);
                transaction.Commit();
            }
        }

        public Product GetById(int id)
        {
            return _session.Get<Product>(id);
        }

        public IList<Product> GetAll()
        {
            return _session.Query<Product>().ToList();
        }

        public void Update(Product product)
        {
            using (var transaction = _session.BeginTransaction())
            {
                // Load the category reference
                if (product.CategoryId > 0)
                {
                    product.Category = _session.Get<Category>(product.CategoryId);
                    if (product.Category == null)
                    {
                        throw new InvalidOperationException($"Category with ID {product.CategoryId} not found");
                    }
                }

                _session.Update(product);
                transaction.Commit();
            }
        }

        public void Delete(int id)
        {
            using (var transaction = _session.BeginTransaction())
            {
                var product = _session.Get<Product>(id);
                if (product != null)
                {
                    _session.Delete(product);
                    transaction.Commit();
                }
            }
        }
    }
}

using FluentNHibernate.Mapping;
using nhibernate_2.Models;

namespace nhibernate_2.Mappings
{
    public class ProductMap : ClassMap<Product>
    {
        public ProductMap()
        {
            Table("Products");
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.Name);
            Map(x => x.Price);
            Map(x => x.Description);
            References(x => x.Category)
                .Column("CategoryId")
                .Not.Nullable()
                .LazyLoad();
        }
    }
}

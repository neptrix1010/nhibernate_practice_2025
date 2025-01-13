using FluentNHibernate.Mapping;
using nhibernate_2.Models;

namespace nhibernate_2.Mappings
{
    public class CategoryMap : ClassMap<Category>
    {
        public CategoryMap()
        {
            Table("Categories");
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.Name);
            Map(x => x.Description);
            HasMany(x => x.Products)
                .Inverse()
                .Cascade.AllDeleteOrphan()
                .KeyColumn("CategoryId");
        }
    }
}

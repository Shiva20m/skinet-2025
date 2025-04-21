using Core.Entities;

namespace Core.Specifications
{
    public class BrandListSpecification:BaseSpecification<Product, string>
    {
        public BrandListSpecification(): base(x => true)
        {
            AddSelect(x=> x.Brand);
            ApplyDistinct();
            
        }
    }
}
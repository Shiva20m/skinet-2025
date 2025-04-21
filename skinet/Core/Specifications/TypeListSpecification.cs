using Core.Entities;

namespace Core.Specifications
{
    public class TypeListSpecification: BaseSpecification<Product, string>
    {
        public TypeListSpecification(): base(x => true)
        {
            AddSelect(x=>x.Type);
            ApplyDistinct();
        }
    }
    
}
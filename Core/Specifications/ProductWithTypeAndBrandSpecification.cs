using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class ProductWithTypeAndBrandSpecification : BaseSpecification<Product>
    {
        public ProductWithTypeAndBrandSpecification()
        {
            AddIncludes(x => x.ProductType);
            AddIncludes(x => x.ProductBrand);
        }
        public ProductWithTypeAndBrandSpecification(ProductSpecParams productParams)
            : base((x) => 
            (string.IsNullOrEmpty(productParams.Search) || x.Name.Contains(productParams.Search))
            && (!productParams.BrandId.HasValue || x.ProductBrandId == productParams.BrandId) 
            && (!productParams.TypeId.HasValue || x.ProductTypeId == productParams.TypeId))
        {
            AddIncludes(x => x.ProductType);
            AddIncludes(x => x.ProductBrand);
            if (!string.IsNullOrEmpty(productParams.Sort))
            {
                switch (productParams.Sort)
                {
                    case"PriceAsc":
                        AddOderBy(x => x.Price);
                        break;
                    case"PriceDesc":
                        AddOrderByDescending(x => x.Price);
                        break;
                    default:
                        AddOderBy(x =>x.Price);
                        break;
                }
            }
            ApplyPaging(productParams.PageIndex, productParams.PageSize);
        }
        public ProductWithTypeAndBrandSpecification(int id): base(x => x.Id ==id)
        {
            AddIncludes(x => x.ProductType);
            AddIncludes(x => x.ProductBrand);
        }
    }
}

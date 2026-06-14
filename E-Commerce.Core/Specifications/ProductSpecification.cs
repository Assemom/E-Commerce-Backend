using E_Commerce.Core.Entities;

namespace E_Commerce.Core.Specifications
{
    public class ProductSpecification : BaseSpecification<Product>
    {
        public ProductSpecification(ProductSpecParams _params) : base(x =>
            (string.IsNullOrEmpty(_params.Search) || x.Name.ToLower().Contains(_params.Search)) &&
            (_params.Brands.Count == 0 || _params.Brands.Contains(x.Brand))
            && (_params.Types.Count == 0 || _params.Types.Contains(x.Type))
        )
        {
            ApplyedPaging(_params._pageSize * (_params.PageIndex - 1), _params.PageSize);

            switch (_params.Sort)
            {
                case "PriceAsc":
                    AddOrderBy(x => x.Price);
                    break;
                case "PriceDesc":
                    AddOrderByDescending(x => x.Price);
                    break;
                default:
                    AddOrderBy(x => x.Name);
                    break;
            }
        }
    }
}

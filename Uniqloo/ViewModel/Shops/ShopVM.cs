using Uniqloo.ViewModel.Brands;
using Uniqloo.ViewModel.Products;

namespace Uniqloo.ViewModel.Shops
{
    public class ShopVM
    {
        public IEnumerable<BrandAndProductVM> Brands { get; set; }
        public IEnumerable<ProductListItemVM> Products { get; set; }
        public int ProductCount { get; set; }
    }
}

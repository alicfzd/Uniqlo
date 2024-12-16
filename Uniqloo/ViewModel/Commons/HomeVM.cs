using Uniqloo.Models;
using Uniqloo.ViewModel.Products;
using Uniqloo.ViewModel.Slider;

namespace Uniqloo.ViewModel.Commons
{
    public class HomeVM
    {
        public IEnumerable<SliderListItemVM> Sliders { get; set; }
        public IEnumerable<ProductListItemVM> PopularProducts { get; set; }
        public List<Brand> Brands { get; internal set; }
    }
}

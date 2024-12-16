using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Uniqloo.DataAccess;
using Uniqloo.ViewModel.Commons;
using Uniqloo.ViewModel.Products;
using Uniqloo.ViewModel.Slider;


namespace Uniqloo.Controllers
{
    public class HomeController(UniqloDbContext _context ) : Controller
    {
        public async Task<IActionResult> Index()
        {
            HomeVM vm = new();
            vm.Sliders = await _context.Sliders.Select(x => new SliderListItemVM

            {
  
                ImageUrl = x.ImageUrl,
                Link = x.Link,
                Subtitle = x.Subtitle,
                Title = x.Title
            }).ToListAsync();
            vm.Brands = await _context.Brands.OrderByDescending(x => x.Products!.Count)
                .Take(4).ToListAsync();
            vm.PopularProducts = await _context.Products
                .Where(x=> vm.Brands.Select(y=>y.Id).Contains(x.BrandId!.Value))
                .Take(10)
                .Select(x=> new ProductListItemVM
                {
                    CoverImage = x.CoverImage,
                    Discount = x.Discount,
                    Id = x.Id,
                    IsInStock = x.Quantity > 0,
                    Name = x.Name,
                    SellPrice = x.SellPrice,
                    BrandId = x.BrandId!.Value
                }).ToListAsync();
            return View(vm);
        }
        public IActionResult About()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult Shop()
        {
            return View();
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}

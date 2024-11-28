using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using Uniqloo.DataAccess;
using Uniqloo.ViewModel.Baskets;
using Uniqloo.ViewModel.Brands;
using Uniqloo.ViewModel.Products;
using Uniqloo.ViewModel.Shops;

namespace Uniqloo.Controllers
{
    public class ShopController(UniqloDbContext _context) : Controller
    {
        public async Task<IActionResult> Index(int? catId, string amount)
        {
            var query = _context.Products.AsQueryable();
            if (catId.HasValue)
            {
                query = query.Where(x => x.BrandId == catId);
            }
            if (amount != null)
            {
                var prices = amount.Split('-').Select(x => Convert.ToInt32(x));
                query = query
                    .Where(y => prices.ElementAt(0) <= y.SellPrice && prices.ElementAt(1) >= y.SellPrice);
            }
            ShopVM vM = new ShopVM();
            vM.Brands = await _context.Brands.
                Where(x => !x.IsDeleted).
                Select(x => new BrandAndProductVM
                {
                    Id = x.Id,
                    Name = x.Name,
                    Count = x.Products.Count

                }).ToListAsync();
            vM.Products = await query.
                Take(6).
                Select(x => new ProductListItemVM
                {
                    CoverImage = x.CoverImage,
                    Discount = x.Discount,
                    Id = x.Id,
                    IsInStock = x.Quantity > 0,
                    Name = x.Name,
                    SellPrice = x.SellPrice
                }).ToListAsync();
            vM.ProductCount = await query.CountAsync();

            return View(vM);
        }
        
        public async Task<IActionResult> AddBasket(int id)
        {
            var basket = getBasket();
            var item = basket.FirstOrDefault(x => x.Id == id);
            if (item == null)
            {
                item.Count++;
            }
            else
            {
                basket.Add(new BasketCookieItemVM
                {
                    Id = id,
                    Count = 1
                });
            }

            string data = JsonSerializer.Serialize(basket);
            HttpContext.Response.Cookies.Append("basket", data);
            return Ok();
        }
        public async Task<IActionResult> GetBasket(int id)
        {
            return Json(getBasket());
        }
        List<BasketCookieItemVM> getBasket()
        {
            try
            {
                string? value = HttpContext.Request.Cookies["basket"];
                if (value is null)
                {
                    return new();
                }
                return JsonSerializer.Deserialize<List<BasketCookieItemVM>>(value) ?? new();
            }
            catch (Exception)
            {

                return new();
            }
            
        }
    }
}

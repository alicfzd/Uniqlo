using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Uniqloo.DataAccess;
using Uniqloo.Extentions;
using Uniqloo.Models;
using Uniqloo.ViewModel.Products;

namespace Uniqloo.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController(IWebHostEnvironment _env, UniqloDbContext _context) : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction(nameof(Create));
        }
        public async Task<IActionResult> Create()
        {
            ViewBag.Catagories = await _context.Brands.Where(x => !x.IsDeleted).ToListAsync();
            SelectList sl = new SelectList(ViewBag.Catagories,"Id","Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateVM vm)
        {
            if (vm.File != null)
            {
                if (!vm.File.IsValidType("image"))
                    ModelState.AddModelError("File", "File must be image");

                if (!vm.File.IsValidSize(400))
                    ModelState.AddModelError("File", "File must be less than 400 kb");
            }

            if (!ModelState.IsValid) return View(vm);
            if (!await _context.Brands.AnyAsync(x => x.Id == vm.BrandId))
            {
                ModelState.AddModelError("BrandId", "Brand tapilmadi");
                return View();
            }
            Product product = vm;
            product.CoverImage = await vm.File!.UploadAsync(_env.WebRootPath, "imgs", "products");
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            

        }

    } 

}

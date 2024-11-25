﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Uniqloo.DataAccess;
using Uniqloo.ViewModel.Slider;

namespace Uniqloo.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController(UniqloDbContext _context, IWebHostEnvironment _env) : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View(await _context.Sliders.ToListAsync());
        }
        
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(SliderCreateVM vm)
        {
            if (!ModelState.IsValid) return View(vm);
            if (!vm.File.ContentType.StartsWith("image")) 
            {
                ModelState.AddModelError("File","Format type must be image"); 
                return View(vm);
            }
            if(vm.File.Length > 2 * 1024 * 1024)
            {
                ModelState.AddModelError("File", "File size must be less than 2 mb");
                return View(vm);
            }
            string newFileName = Path.GetRandomFileName() + Path.GetExtension(vm.File.FileName);
            using (Stream stream = System.IO.File.Create(Path.Combine(_env.WebRootPath, "imgs", newFileName))) 
            {
                await vm.File.CopyToAsync(stream);
            }

            return View(vm);
        }
    }
}

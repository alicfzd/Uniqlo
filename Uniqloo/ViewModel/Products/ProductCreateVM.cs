﻿using System.ComponentModel.DataAnnotations;
using Uniqloo.Models;


namespace Uniqloo.ViewModel.Products
{
    public class ProductCreateVM
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal CostPrice { get; set; }
        public decimal SellPrice { get; set; }
        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }
        [Range(0, 100)]
        public int Discount { get; set; }
        public int BrandId { get; set; }
        public IFormFile File { get; set; }
        public ICollection<IFormFile>? OtherFiles { get; set; }

        public static implicit operator Product(ProductCreateVM vm)
        {
            return new Product
            {
                BrandId = vm.BrandId,
                CostPrice = vm.CostPrice,
                Description = vm.Description,
                Discount = vm.Discount,
                Name = vm.Name,
                Quantity = vm.Quantity,
                SellPrice = vm.SellPrice
            };
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Products.Queries.GetAllProducts
{
    public class GetAllProductsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Barcode { get; set; }
        public string Description { get; set; }
        public decimal Rate { get; set; }
        public decimal RemanigStock { get; set; }
        public decimal MonthlySale { get; set; }
    }
}

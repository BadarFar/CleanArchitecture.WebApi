using Application.Features.Products.Queries.GetAllProducts;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Domain.Entities;
using System.Linq;

namespace Application.Features.SaleStock.Queries
{
    public class GetAllProductsSaleAndStockQuery : IRequest<IEnumerable<GetAllProductsViewModel>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }

    public class GetAllProductsSaleAndStockHandler : IRequestHandler<GetAllProductsSaleAndStockQuery, IEnumerable<GetAllProductsViewModel>>
    {
        private readonly IProductRepositoryAsync _productRepository;
        private readonly ISaleDetailRepositoryAsync _saleDetailRepository;
        private readonly IMapper _mapper;
        public GetAllProductsSaleAndStockHandler(IProductRepositoryAsync productRepository,
            ISaleDetailRepositoryAsync saleDetailRepository,
            IMapper mapper)
        {
            _productRepository = productRepository;
            _saleDetailRepository = saleDetailRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetAllProductsViewModel>> Handle(GetAllProductsSaleAndStockQuery request, CancellationToken cancellationToken)
        {
            var productList = new List<GetAllProductsViewModel>();
            var products = await _productRepository.GetAllAsync(); // Assume GetProductsAsync returns a list of Product entities

            foreach (var product in products)
            {
                var productViewModel = _mapper.Map<GetAllProductsViewModel>(product);
                productViewModel.MonthlySale = await CalculateTotalMonthlySaleAsync(product);
                productViewModel.RemanigStock = await CalculateRemainingStockAsync(product);

                productList.Add(productViewModel);

            }

            return productList;
        }

        //public async Task<IEnumerable<GetAllProductsViewModel>> Handle(GetAllProductsSaleAndStockQuery request, CancellationToken cancellationToken)
        //{
        //    throw new NotImplementedException();
        //}


        private async Task<decimal> CalculateTotalMonthlySaleAsync(Product product)
        {
            try
            {
                // Logic to calculate total monthly sale for a product
                var sales = await _saleDetailRepository.GetSalesByProductAsync(product.Id);

                // Assuming you have a Date property in SaleMaster to filter by month
                var totalMonthlySale = sales.Sum(sale => sale.Qty * sale.Rate);

                return totalMonthlySale;
            }
            catch (NullReferenceException ex)
            {
                throw new NullReferenceException(ex.Message, ex);
            }
        }

        private async Task<decimal> CalculateRemainingStockAsync(Product product)
        {
            // Logic to calculate remaining stock for a product
            var totalSalesQty = (await _saleDetailRepository.GetSalesByProductAsync(product.Id)).Sum(sale => sale.Qty);
            var remainingStock = product.Stock - totalSalesQty;

            return remainingStock;
        }
    }


}

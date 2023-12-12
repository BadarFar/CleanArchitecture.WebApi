using Application.Features.Products.Queries.GetAllProducts;
using Application.Features.SaleStock.Queries;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;

namespace Application.Features.Discount.Queries
{
    public class GetDiscountOnProductQuery : IRequest<SaleMaster>
    {
        public SaleMaster SaleMaster { get; set; }
    }

    public class GetDiscountOnProductHandler : IRequestHandler<GetDiscountOnProductQuery, SaleMaster>
    {
        private readonly IProductRepositoryAsync _productRepository;
        private readonly ISaleDetailRepositoryAsync _saleDetailRepository;
        private readonly IMapper _mapper;
        public GetDiscountOnProductHandler(IProductRepositoryAsync productRepository,
            ISaleDetailRepositoryAsync saleDetailRepository,
            IMapper mapper)
        {
            _productRepository = productRepository;
            _saleDetailRepository = saleDetailRepository;
            _mapper = mapper;
        }

        public Task<SaleMaster> Handle(GetDiscountOnProductQuery request, CancellationToken cancellationToken)
        {
            foreach (var saleDetail in request.SaleMaster.SaleDetails)
            {

                // Apply discount logic based on different conditions
                decimal discountPercentage = CalculateDiscountPercentage(request.SaleMaster);

                // Apply the discount to the sale detail
                ApplyDiscountToSaleDetail(saleDetail, discountPercentage);

            }

            return Task.FromResult(request.SaleMaster);
        }

        private decimal CalculateDiscountPercentage(SaleMaster sale)
        {
            // Initialize discount percentage based on different conditions
            decimal discountPercentage = 0;

            // Condition 1: If total sale > 1500, apply a 5% discount
            if (sale.SaleDetails.Sum(sd => sd.Rate * sd.Qty) > 1500)
            {
                discountPercentage += 0.05m;
            }
            // Condition 2: If total sale > 1000, apply a 2% discount
            else if (sale.SaleDetails.Sum(sd => sd.Rate * sd.Qty) > 1000)
            {
                discountPercentage += 0.02m;
            }

            // Condition 3: If individual product quantity is greater than 10, apply a 10% discount
            if (sale.SaleDetails[2].Qty == 10)
            {
                discountPercentage += 0.1m;
            }

            return discountPercentage;
        }

        private void ApplyDiscountToSaleDetail(SaleDetail saleDetail, decimal discountPercentage)
        {
            // Calculate the discounted total based on the discount percentage
            decimal discountedTotal = saleDetail.Total.GetValueOrDefault() * (1 - discountPercentage);

            // Update the SaleDetail with the discounted total
            saleDetail.Total = discountedTotal;
            saleDetail.Discount = discountPercentage;
        }
    }


}

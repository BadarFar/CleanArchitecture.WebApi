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
            throw new NotImplementedException();
        }
    }


}

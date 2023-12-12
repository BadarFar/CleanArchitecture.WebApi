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
            throw new NotImplementedException();
        }
    }

}

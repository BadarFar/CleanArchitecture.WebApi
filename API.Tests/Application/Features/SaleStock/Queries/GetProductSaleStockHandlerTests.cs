using AutoFixture.Xunit2;
using AutoFixture;
using Moq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Application.Interfaces.Repositories;
using Application.Features.SaleStock.Queries;
using AutoMapper;
using Domain.Entities;
using Application.Features.Products.Queries.GetAllProducts;
using Application.Exceptions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace API.Tests.Application.Features.SaleStock.Queries
{
    public class GetProductSaleStockHandlerTests
    {
        [Theory, AutoMoqData]
        public async Task Sut_GetAllProductsSaleAndStock(
        [Frozen] Mock<IProductRepositoryAsync> productRepository,
        [Frozen] Mock<ISaleDetailRepositoryAsync> saleDetailRepository,
        [Frozen] Mock<IMapper> mapper,
        GetAllProductsSaleAndStockHandler sut)
        {
            
        }


        [Theory, AutoMoqData]
        public async Task Sut_ArgumentNullException(
        [Frozen] Mock<IProductRepositoryAsync> productRepository,
        [Frozen] Mock<ISaleDetailRepositoryAsync> saleDetailRepository,
        [Frozen] Mock<IMapper> mapper,
        GetAllProductsSaleAndStockHandler sut)
        {

        }
    }
}

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
            var fixture = new Fixture();
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            var saleAndStockQuery = fixture.Create<GetAllProductsSaleAndStockQuery>();
            var products = fixture.CreateMany<Product>().ToList();
            var productsViewModel = fixture.CreateMany<GetAllProductsViewModel>().ToList();
            var saleDetails = fixture.CreateMany<SaleDetail>().ToList();
            saleDetails[0].ProductId = products[0].Id;
            saleDetails[0].Product = products[0];
            saleDetails[1].ProductId = products[1].Id;
            saleDetails[1].Product = products[1];
            saleDetails[2].ProductId = products[2].Id;
            saleDetails[2].Product = products[2];

            //mock db calls
            productRepository.Setup(p => p.GetAllAsync()).ReturnsAsync(products);
            mapper.SetupSequence(mapper => mapper.Map<GetAllProductsViewModel>(It.IsAny<Product>()))
                .Returns(productsViewModel[0])
                .Returns(productsViewModel[1])
                .Returns(productsViewModel[2]);

            saleDetailRepository.Setup(s => s.GetSalesByProductAsync(It.IsAny<int>())).ReturnsAsync(saleDetails);

            var result = await sut.Handle(saleAndStockQuery, new CancellationToken());

            Assert.Equal(productsViewModel.FirstOrDefault().Name, result.FirstOrDefault().Name);
            Assert.NotEqual(0, result.FirstOrDefault().RemanigStock);
            Assert.NotEqual(0, result.FirstOrDefault().MonthlySale);
        }


        [Theory, AutoMoqData]
        public async Task Sut_NullRefrenceException(
        [Frozen] Mock<IProductRepositoryAsync> productRepository,
        [Frozen] Mock<ISaleDetailRepositoryAsync> saleDetailRepository,
        [Frozen] Mock<IMapper> mapper,
        GetAllProductsSaleAndStockHandler sut)
        {
            var fixture = new Fixture();
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            var saleAndStockQuery = fixture.Create<GetAllProductsSaleAndStockQuery>();
            var products = fixture.CreateMany<Product>().ToList();
            var productsViewModel = fixture.CreateMany<GetAllProductsViewModel>().ToList();
            var saleDetails = fixture.CreateMany<SaleDetail>().ToList();

            //mock db calls
            productRepository.Setup(p => p.GetAllAsync()).ReturnsAsync(products);
            mapper.SetupSequence(mapper => mapper.Map<GetAllProductsViewModel>(It.IsAny<Product>()))
                .Returns(productsViewModel[0])
                .Returns(productsViewModel[1])
                .Returns(productsViewModel[2]);

            saleDetailRepository.Setup(s => s.GetSalesByProductAsync(It.IsAny<int>()))
                .ThrowsAsync(new NullReferenceException("Sale Not Found!"));

            Assert.ThrowsAsync<NullReferenceException>(async () => await sut.Handle(saleAndStockQuery, new CancellationToken()));
            saleDetailRepository.Verify(x => x.GetSalesByProductAsync(It.IsAny<int>()), Times.Once);
        }
    }
}

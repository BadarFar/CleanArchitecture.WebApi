using Application.Features.Products.Commands.CreateProduct;
using Application.Interfaces.Repositories;
using Application.Mappings;
using AutoMapper;
using Domain.Entities;
using Moq;
using Xunit;

namespace API.Tests.Application.Features.Products.Commands
{
    public class CreateProductHandlerTests
    {
        [Fact]
        public async Task CreateProductHandler_Should_Add_Product()
        {
            // Arrange
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new GeneralProfile());
            });

            var mapper = mapperConfig.CreateMapper();

            var repository = new Mock<IProductRepositoryAsync>();

            var command = new CreateProductCommand
            {
                Name = "Sony WH-1000XM5",
                Description = "Sony noise cancelling headphones",
                Barcode = "q834729234",
                Rate = 329
            };

            repository
                .Setup(x => x.AddAsync(It.Is<Product>(x =>
                    x.Name == command.Name
                    && x.Description == command.Description
                    && x.Barcode == command.Barcode
                    && x.Rate == command.Rate
                )))
                .Callback<Product>((p) =>
                {
                    p.Id = 35;
                });

            var handler = new CreateProductCommandHandler(repository.Object, mapper);

            // Act
            var result = await handler.Handle(command, new CancellationToken());

            // Assert
            Assert.True(result.Succeeded);
            Assert.Equal(35, result.Data);
        }
    }
}

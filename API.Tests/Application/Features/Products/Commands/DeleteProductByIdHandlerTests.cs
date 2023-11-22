using Application.Exceptions;
using Application.Features.Products.Commands.CreateProduct;
using Application.Features.Products.Commands.DeleteProductById;
using Application.Interfaces.Repositories;
using Domain.Entities;
using Moq;
using Xunit;
using static Application.Features.Products.Commands.DeleteProductById.DeleteProductByIdCommand;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace API.Tests.Application.Features.Products.Commands
{
    public class DeleteProductByIdHandlerTests
    {
        [Fact]
        public async Task DeleteProductByIdHandler_Should_Throw_ApiException_When_Product_Not_Found()
        {
            // Arrange
            var repository = new Mock<IProductRepositoryAsync>();

            var command = new DeleteProductByIdCommand
            {
                Id = 2
            };

            repository
                .Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Product)null!);


            var handler = new DeleteProductByIdCommandHandler(repository.Object);

            // Act

            // Assert
            _ = Assert.ThrowsAsync<ApiException>(async () => await handler.Handle(command, new CancellationToken()));
        }


        [Fact]
        public async Task DeleteProductByIdHandler_Should_Delte_Product()
        {
            // Arrange
            var repository = new Mock<IProductRepositoryAsync>();
            var product = new Product { Id = 2, Name = "Product 2" };

            var command = new DeleteProductByIdCommand
            {
                Id = 2
            };

            repository
                .Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(product);


            repository.Setup(x => x.DeleteAsync(It.Is<Product>(i => i.Id == 2 && i.Name == "Product 2")))
                .Returns(Task.FromResult(product));


            var handler = new DeleteProductByIdCommandHandler(repository.Object);

            // Act
            var result = await handler.Handle(command, new CancellationToken());


            // Assert
            Assert.True(result.Succeeded);
            Assert.Equal(2, result.Data);

        }
    }
}

using Application.Exceptions;
using Application.Features.Products.Queries.GetProductById;
using Application.Interfaces.Repositories;
using Moq;
using Xunit;
using static Application.Features.Products.Queries.GetProductById.GetProductByIdQuery;

namespace API.Tests.Application.Features.Products.Queries
{
    public class GetProductByIdHandlerTests
    {
        [Fact]
        public async Task GetProductByIdHandler_Handle_Should_Throw_Exception_When_Product_Not_Found()
        {
        }

        [Fact]
        public async Task GetProductByIdHandler_Handle_Should_Return_Product_For_Given_Id()
        {
            // Arrange
            var repositoryMoq = new Mock<IProductRepositoryAsync>();
            var query = new GetProductByIdQuery
            {
                Id = 1,
            };

            var product = new Domain.Entities.Product
            {
                Id = 1,
                Name = "Product 1",
                Rate = 10,
                Barcode = "29147619246",
                Description = "Product 1 description"
            };

            repositoryMoq.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(product);

            var handler = new GetProductByIdQueryHandler(repositoryMoq.Object);

            // Act
            var result = await handler.Handle(query, new CancellationToken());

            // Act
            Assert.Equal(result.Data.Id, product.Id);
            Assert.Equal(result.Data.Name, product.Name);
            Assert.Equal(result.Data.Rate, product.Rate);
            Assert.Equal(result.Data.Rate, product.Rate);
            Assert.Equal(result.Data.Barcode, product.Barcode);
            Assert.Equal(result.Data.Description, product.Description);
        }
    }
}

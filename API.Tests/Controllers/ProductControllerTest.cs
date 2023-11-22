using API.Controllers;
using Application.Exceptions;
using Application.Features.Products.Commands.CreateProduct;
using Application.Features.Products.Queries.GetAllProducts;
using Application.Features.Products.Queries.GetProductById;
using Application.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace API.Tests.Controllers
{
    public class ProductControllerTest
    {
        [Fact]
        public async Task ProducController_Get_Should_Call_Mediator()
        {
            // Arrange
            // Setup the  dependencies
            var mediator = new Mock<IMediator>();
            var filter = new GetAllProductsParameter { PageNumber = 1, PageSize = 10 };

            var pagedResponse = new PagedResponse<IEnumerable<GetAllProductsViewModel>>(new List<GetAllProductsViewModel>(), 1, 10);

            mediator
                .Setup(x => x.Send(It.IsAny<GetAllProductsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(pagedResponse);

            var controller = new ProductController(mediator.Object);

            // Act
            // Perform the action (call the method)
            var response = await controller.Get(filter);

            // Assert
            // Compare actual result with expected result
            var result = response as OkObjectResult;
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.IsType<PagedResponse<IEnumerable<GetAllProductsViewModel>>>(result.Value);
        }

        [Fact]
        public async Task ProducController_Get_By_Id_Should_Throw_Api_Exception_When_No_Data_Found()
        {
            // Arrange
            // Setup the  dependencies
            var mediator = new Mock<IMediator>();

            mediator
                .Setup(x => x.Send(It.Is<GetProductByIdQuery>(c => c.Id == 16), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new ApiException());

            var controller = new ProductController(mediator.Object);

            // Act

            // Assert
            // Compare actual result with expected result
            _ = Assert.ThrowsAsync<ApiException>(async () => await controller.Get(16));
        }


        [Fact]
        public async Task ProducController_Post_Should_Call_Mediator()
        {
            // Arrange
            // Setup the  dependencies
            var mediator = new Mock<IMediator>();
            var command = new CreateProductCommand { Name = "Test Product", Description = "TDD Training", Rate = 10, Barcode = "89123719237" };


            mediator
                .Setup(x => x.Send(It.IsAny<CreateProductCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Response<int>(1, "Product added successfully"));

            var controller = new ProductController(mediator.Object);

            // Act
            // Perform the action (call the method)
            var response = await controller.Post(command);

            // Assert
            // Compare actual result with expected result
            var result = response as OkObjectResult;
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.IsType<Response<int>>(result.Value);
        }
    }
}

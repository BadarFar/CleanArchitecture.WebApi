using Application.Features.Products.Queries.GetAllProducts;
using Application.Features.SaleStock.Queries;
using Application.Interfaces.Repositories;
using AutoFixture.Xunit2;
using AutoFixture;
using AutoMapper;
using Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Application.Features.Discount.Queries;

namespace API.Tests.Application.Features.SaleStock.Queries
{
    public class GetDiscountOnProductHandlerTest
    {
        [Theory]
        [InlineAutoMoqData(0, 1500, 2, 0.05)]
        [InlineAutoMoqData(1, 1001, 1, 0.02)]
        [InlineAutoMoqData(2, 100, 10, 0.12)]
        public async Task Sut_GetDiscountOnProductHandler(int i, int productPrice, int qty, decimal per,
        [Frozen] Mock<IProductRepositoryAsync> productRepository,
        [Frozen] Mock<ISaleDetailRepositoryAsync> saleDetailRepository,
        [Frozen] Mock<IMapper> mapper,
        GetDiscountOnProductHandler sut)
        {
            var fixture = new Fixture();
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            var query = fixture.Create<GetDiscountOnProductQuery>();

            if (productPrice == 1500)
            {
                query.SaleMaster.SaleDetails[0].Rate = productPrice;
                query.SaleMaster.SaleDetails[0].Qty = qty;
            }
            else if (productPrice == 1001)
            {
                query.SaleMaster.SaleDetails[0].Rate = 1;
                query.SaleMaster.SaleDetails[0].Qty = 1;
                query.SaleMaster.SaleDetails[1].Rate = productPrice;
                query.SaleMaster.SaleDetails[1].Qty = qty;
                query.SaleMaster.SaleDetails[2].Rate = 1;
                query.SaleMaster.SaleDetails[2].Qty = 1;
            }
            else
            {
                query.SaleMaster.SaleDetails[0].Rate = 1;
                query.SaleMaster.SaleDetails[0].Qty = 1;
                query.SaleMaster.SaleDetails[1].Rate = 1;
                query.SaleMaster.SaleDetails[1].Qty = 1;
                query.SaleMaster.SaleDetails[2].Rate = productPrice;
                query.SaleMaster.SaleDetails[2].Qty = qty;
            }

            var result = await sut.Handle(query, new CancellationToken());

            Assert.Equal(result.SaleDetails[i].Discount, per);
        }
    }
}

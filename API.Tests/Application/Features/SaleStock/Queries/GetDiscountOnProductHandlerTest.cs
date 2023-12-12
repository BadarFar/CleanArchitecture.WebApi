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
        [InlineAutoMoqData(1, 1500, 2, 0.05)]
        [InlineAutoMoqData(2, 1000, 1, 0.02)]
        [InlineAutoMoqData(3, 10, 10, 0.1)]
        public async Task Sut_GetDiscountOnProductHandler(int i, int productPrice, int qty, decimal per,
        GetDiscountOnProductHandler sut)
        {
 
        }
    }
}

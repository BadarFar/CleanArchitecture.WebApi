using Application.Features.Basic;
using Application.Interfaces;
using AutoFixture.Xunit2;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace API.Tests.Application.Basic
{
    public class BasicTests
    {

        [Fact]
        public void AddTwoNumberTest()
        {

            var num1 = 6;
            var num2 = 4;
            var total = 12;

            Mock<IMath> mockMath = new Mock<IMath>();
            mockMath.Setup(m => m.Add(It.IsAny<int>(), It.IsAny<int>())).Returns(total);

            Fibonacci fibonacci = new Fibonacci(mockMath.Object);
            var result = fibonacci.GetNthTerm(4);

            Assert.Equal(result, total);
        }

        [Theory, AutoMoqData]
        public void AddTwoNumberWithParamTest([Frozen] Mock<IMath> mockMath,
            Fibonacci sut)
        {

            var num1 = 6;
            var num2 = 4;
            var total = 12;

            mockMath.Setup(m => m.Add(It.IsAny<int>(), It.IsAny<int>())).Returns(total);
            var result = sut.GetNthTerm(4);

            Assert.Equal(result, total);
        }

        [Theory]
        [InlineAutoMoqData(6, 6, 12)]
        [InlineAutoMoqData(5, 5, 10)]
        public void AddTwoNumberWithInlineAutoMokTest(int input1, int input2, int total,
            [Frozen] Mock<IMath> mockMath,
            Fibonacci sut)
        {

            mockMath.Setup(m => m.Add(It.IsAny<int>(), It.IsAny<int>())).Returns(input1 + input2);
            var result = sut.GetNthTerm(4);

            Assert.Equal(result, total);
        }

    }
}

using AutoFixture;
using AutoFixture.AutoMoq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Tests
{
    public class DefaultCustomization : CompositeCustomization
    {
        public DefaultCustomization() : base(new AutoMoqCustomization())
        {
        }
    }
}

using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Basic
{
    public class MathOperations : IMath
    {
        public int Add(int x, int y)
        {
            return x + y;
        }
    }
}

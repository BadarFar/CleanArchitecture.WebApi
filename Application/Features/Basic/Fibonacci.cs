using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Basic
{
    public class Fibonacci : IFibonacci
    {
        private IMath _math;
        public Fibonacci(IMath math)
        {
            _math = math;
        }

        public int GetNthTerm(int n)
        {
            int nMinusTwoTerm = 1;
            int nMinusOneTerm = 1;
            int newTerm = 0;
            for (int i = 2; i < n; i++)
            {
                newTerm = _math.Add(nMinusOneTerm, nMinusTwoTerm);
                nMinusTwoTerm = nMinusOneTerm;
                nMinusOneTerm = newTerm;
            }

            return newTerm;
        }
    }
}

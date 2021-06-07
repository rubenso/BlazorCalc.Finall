using System;
using BlazorCalc.Business.Contracts;

namespace BlazorCalc.Business.Operators
{
    public class DivideOperator : IOperator
    {
        public char Type { get; } = '/';

        public double ExecuteOperation(double number1, double number2)
        {
            if (number2 == 0)
                throw new DivideByZeroException($"Can't divide by 0: {number1}{Type}{number2}");

            return number1 / number2;
        }
    }
}
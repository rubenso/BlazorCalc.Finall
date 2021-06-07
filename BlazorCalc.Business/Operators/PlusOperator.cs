using BlazorCalc.Business.Contracts;

namespace BlazorCalc.Business.Operators
{
    public class PlusOperator : IOperator
    {
        public char Type { get; } = '+';

        public double ExecuteOperation(double number1, double number2)
        {
            return number1 + number2;
        }
    }
}
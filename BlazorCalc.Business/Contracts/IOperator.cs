namespace BlazorCalc.Business.Contracts
{
    public interface IOperator
    {
        public char Type { get; }

        public double ExecuteOperation(double number1, double number2);
    }
}
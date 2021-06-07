using System;
using System.Collections.Generic;
using System.Linq;
using BlazorCalc.Business.Classes;
using BlazorCalc.Business.Contracts;

namespace BlazorCalc.Business
{
    public class CalculationService : IBusinessService
    {
        private readonly IEnumerable<IOperator> _operators;

        public CalculationService(IEnumerable<IOperator> operators)
        {
            _operators = operators;
        }

        public SendErrorDelegate SendError { get; set; }

        public double Calculate(IDictionary<int, string> data)
        {
            try
            {
                var number1 = Convert.ToDouble(data[1]);
                if (data.Count < 3)
                    return number1;
                var number2 = Convert.ToDouble(data[3]);
                var operation = Convert.ToChar(data[2]);

                var executor = _operators.FirstOrDefault(o => o.Type == operation);
                if (executor == null)
                    throw new NotImplementedException($"Operator for {operation} not implemented");

                return executor.ExecuteOperation(number1, number2);
            }

            catch (Exception ex)
            {
                SendError?.Invoke(ex);
                throw;
            }
        }
    }
}
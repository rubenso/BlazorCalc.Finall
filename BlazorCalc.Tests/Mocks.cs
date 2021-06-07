using System;
using System.Collections.Generic;
using BlazorCalc.Business.Contracts;
using BlazorCalc.Business.Operators;

namespace BlazorCalc.Tests
{
    public static class Mocks
    {
        public static readonly Dictionary<int, string> SuccessPlusDictionary = new Dictionary<int, string>
        {
            {1, "1"},
            {2, "+"},
            {3, "2"}
        };

        public static readonly Dictionary<int, string> SuccessMinusDictionary = new Dictionary<int, string>
        {
            {1, "5"},
            {2, "-"},
            {3, "3"}
        };

        public static readonly Dictionary<int, string> SuccessDivideDictionary = new Dictionary<int, string>
        {
            {1, "2"},
            {2, "/"},
            {3, "2"}
        };

        public static readonly Dictionary<int, string> SuccessMultiplyDictionary = new Dictionary<int, string>
        {
            {1, "3"},
            {2, "*"},
            {3, "2"}
        };

        public static readonly Dictionary<int, string> ZeroDivideDictionary = new Dictionary<int, string>
        {
            {1, "1"},
            {2, "/"},
            {3, "0"}
        };

        public static readonly Dictionary<int, string> NotImplementedOperatorDictionary = new Dictionary<int, string>
        {
            {1, "1"},
            {2, ">"},
            {3, "0"}
        };

        public static IEnumerable<IOperator> RegisteredOperators()
        {
            //Arrange
            IOperator operatorPlus = new PlusOperator();
            IOperator operatorMinus = new MinusOperator();
            IOperator operatorDivide = new DivideOperator();
            IOperator operatorMultiply = new MultiplyOperator();

            return new[]
            {
                operatorPlus, operatorMinus, operatorDivide, operatorMultiply
            };
        }

        public static void SendError(Exception exc)
        {
        }
    }
}
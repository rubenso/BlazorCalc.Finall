using System;
using BlazorCalc.Business;
using BlazorCalc.Business.Contracts;
using BlazorCalc.Server.Parsers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlazorCalc.Tests
{
    [TestClass]
    public class CalculationTest
    {
        [TestMethod]
        public void TestCalculation()
        {
            IBusinessService calcService = new CalculationService(Mocks.RegisteredOperators());

            Assert.AreEqual(calcService.Calculate(Mocks.SuccessPlusDictionary), 3);
            Assert.AreEqual(calcService.Calculate(Mocks.SuccessMinusDictionary), 2);
            Assert.AreEqual(calcService.Calculate(Mocks.SuccessMultiplyDictionary), 6);
            Assert.AreEqual(calcService.Calculate(Mocks.SuccessDivideDictionary), 1);
        }

        [TestMethod]
        public void TestDivideByZero()
        {
            IBusinessService calcService = new CalculationService(Mocks.RegisteredOperators());

            Assert.ThrowsException<DivideByZeroException>
                (() => calcService.Calculate(Mocks.ZeroDivideDictionary));
        }

        [TestMethod]
        public void TestNotImplementedOperator()
        {
            IBusinessService calcService = new CalculationService(Mocks.RegisteredOperators());

            Assert.ThrowsException<NotImplementedException>
                (() => calcService.Calculate(Mocks.NotImplementedOperatorDictionary));
        }

        [TestMethod]
        public void TestInputParsing()
        {
            var inputParser = new InputParser();
            var inputValue = "1+2";

            var result = inputParser.Parse(inputValue);
            Assert.AreEqual($"{result[1]}{result[2]}{result[3]}", inputValue);
        }

        [TestMethod]
        public void TestErrorSend()
        {
            IBusinessService calcService = new CalculationService(Mocks.RegisteredOperators());
            calcService.SendError += Mocks.SendError;
            calcService.SendError(new NotImplementedException("Send error"));
            Assert.IsNotNull(calcService.SendError);
        }
    }
}
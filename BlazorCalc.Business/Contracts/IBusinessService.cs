using System.Collections.Generic;
using BlazorCalc.Business.Classes;

namespace BlazorCalc.Business.Contracts
{
    public interface IBusinessService
    {
        public SendErrorDelegate SendError { get; set; }
        public double Calculate(IDictionary<int, string> data);
    }
}
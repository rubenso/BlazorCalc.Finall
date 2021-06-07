using System;
using System.Globalization;

namespace BlazorCalc.Server.Parsers
{
    public class OutputParser
    {
        public string Parse(double number, int? decimals)
        {
            return decimals is null
                ? Math.Round(number, MidpointRounding.AwayFromZero)
                    .ToString(CultureInfo.InvariantCulture)
                : number.ToString(CultureInfo.InvariantCulture);
        }
    }
}
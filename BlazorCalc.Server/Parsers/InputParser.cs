using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace BlazorCalc.Server.Parsers
{
    public class InputParser
    {
        public Dictionary<int, string> Parse(string expression)
        {
            var firstNumberSymbol = string.Empty;
            if (expression.StartsWith('-'))
            {
                expression = expression.Remove(0, 1);
                firstNumberSymbol = "-";
            }

            if (expression.StartsWith(',') ||
                expression.StartsWith('.'))
                expression = $"0{expression}";
            var split = Regex.Split(expression, @"[-+*/]");

            var result = new Dictionary<int, string>();

            var part1 = split[0];
            result.Add(1, firstNumberSymbol + split[0]);
            if (split.Count() == 1) return result;

            var part2 = split[1];

            result.Add(2, expression.Replace(part1, null)
                .Replace(part2, null));

            result.Add(3, part2);


            return result;
        }
    }
}
namespace BlazorCalc.Shared
{
    public class Expression
    {
        public string Value { get; set; }
        public bool Round { get; set; }
        public bool FailsToHistory { get; set; }
    }
}
namespace _02.CLIWEB.Models
{
    public class ConversionResult
    {
        public double Input { get; set; }
        public double Output { get; set; }
        public string FromUnit { get; set; } = "";
        public string ToUnit { get; set; } = "";
    }
}

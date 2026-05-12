using _04.CLICON.Models;
using _04.CLICON.Services;
using _04.CLICON.Views;

namespace _04.CLICON.Controllers
{
    public class ConversionController
    {
        private readonly ConsoleView _view;
        private readonly ApiClient _api;

        public ConversionController(ConsoleView view, ApiClient api)
        {
            _view = view; _api = api;
        }

        public bool MenuLoop(string username)
        {
            _view.ShowMenu(username);
            var op = _view.AskOption();
            if (op == "0") return false;

            try
            {
                switch (op)
                {
                    // Dentro del switch de MenuLoop:
                    case "1": DoConvert(ConversionType.MetroAPie, "m", "ft", "Metros: "); break;
                    case "2": DoConvert(ConversionType.KilometroAMilla, "km", "mi", "Kilómetros: "); break;
                    case "3": DoConvert(ConversionType.CentimetroAPulgada, "cm", "in", "Centímetros: "); break;
                    case "4": DoConvert(ConversionType.PulgadaACentimetro, "in", "cm", "Pulgadas: "); break;
                    case "5": DoConvert(ConversionType.PieAMetro, "ft", "m", "Pies: "); break;
                    case "6": DoConvert(ConversionType.KilogramoALibra, "kg", "lb", "Kilogramos: "); break;
                    case "7": DoConvert(ConversionType.GramoAOnza, "g", "oz", "Gramos: "); break;
                    case "8": DoConvert(ConversionType.ToneladaAKilogramo, "t", "kg", "Toneladas: "); break;
                    case "9": DoConvert(ConversionType.LibraAKilogramo, "lb", "kg", "Libras: "); break;
                    case "10": DoConvert(ConversionType.OnzaAGramo, "oz", "g", "Onzas: "); break;
                    case "11": DoConvert(ConversionType.CelsiusAFahrenheit, "°C", "°F", "Celsius: "); break;
                    case "12": DoConvert(ConversionType.CelsiusAKelvin, "°C", "K", "Celsius: "); break;
                    case "13": DoConvert(ConversionType.CelsiusARankine, "°C", "°Ra", "Celsius: "); break;
                    case "14": DoConvert(ConversionType.CelsiusAReaumur, "°C", "°Re", "Celsius: "); break;
                    case "15": DoConvert(ConversionType.FahrenheitACelsius, "°F", "°C", "Fahrenheit: "); break;
                }
            }
            catch (System.Exception ex)
            {
                _view.ShowError($"Error al invocar el servicio: {ex.Message}");
            }

            return true;
        }

        private void DoConvert(ConversionType type, string from, string to, string prompt)
        {
            double val = _view.AskValue(prompt);
            double outVal = _api.Convert(type, val);
            _view.ShowConversion(new ConversionResult { Input = val, Output = outVal, FromUnit = from, ToUnit = to });
        }
    }
}

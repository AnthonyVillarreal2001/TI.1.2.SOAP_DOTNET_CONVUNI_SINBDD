using _02.CLIESC.Models;
using _02.CLIESC.Services;
using _02.CLIESC.Views;
using System;
using System.Windows.Forms;

namespace _02.CLIESC.Controllers
{
    public class ConversionController
    {
        private readonly ConvertForm _view;
        private readonly UserSession _session;
        private readonly ApiClient _api = new ApiClient();

        public ConversionController(ConvertForm view, UserSession session)
        {
            _view = view; _session = session;

            _view.BtnConvert.Click += (s, e) =>
            {
                var input = _view.TryGetInput();
                if (input == null) return;

                int conversionId = _view.ObtenerIdConversion();
                var type = (ConversionType)conversionId;
                try
                {
                    double result = _api.Convert(type, input.Value);
                    string toUnit = GetToUnit(type);
                    _view.ShowResult(result.ToString("F6") + " " + toUnit);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al invocar el servicio:\n" + ex.Message,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            _view.BtnLogout.Click += (s, e) =>
            {
                var confirm = MessageBox.Show("¿Está seguro de cerrar sesión?", "Confirmar", MessageBoxButtons.YesNo);
                if (confirm == DialogResult.Yes)
                {
                    _session.SignOut();
                    _view.Close();
                }
            };
        }

        private static string GetToUnit(ConversionType t)
        {
            switch (t)
            {
                case ConversionType.MetroAPie: return "ft";
                case ConversionType.KilometroAMilla: return "mi";
                case ConversionType.CentimetroAPulgada: return "in";
                case ConversionType.PulgadaACentimetro: return "cm";
                case ConversionType.PieAMetro: return "m";
                case ConversionType.KilogramoALibra: return "lb";
                case ConversionType.GramoAOnza: return "oz";
                case ConversionType.ToneladaAKilogramo: return "kg";
                case ConversionType.LibraAKilogramo: return "kg";
                case ConversionType.OnzaAGramo: return "g";
                case ConversionType.CelsiusAFahrenheit: return "°F";
                case ConversionType.CelsiusAKelvin: return "K";
                case ConversionType.CelsiusARankine: return "°Ra";
                case ConversionType.CelsiusARreaumur: return "°Re";
                case ConversionType.FahrenheitACelsius: return "°C";
                default: return "";
            }
        }
    }
}
using System.ServiceModel;
using _02.CLIESC.Models;
using _02.CLIESC.ConversionServiceReference;

namespace _02.CLIESC.Services
{
    public class ApiClient
    {
        private readonly ConversionServiceClient _client;

        // En _02.CLIESC.Services.ApiClient.cs
        public bool ValidateLogin(string user, string pass)
        {
            return _client.Login(user, pass);
        }

        public ApiClient()
        {
            // Ajusta la URL a donde corre tu WCF:
            var serviceUrl = "http://localhost:57003/ConversionService.svc";

            var binding = new BasicHttpBinding
            {
                MaxReceivedMessageSize = 65536
            };
            var address = new EndpointAddress(serviceUrl);

            _client = new ConversionServiceClient(binding, address);
        }

        public double Convert(ConversionType type, double value)
        {
            switch (type)
            {
                // Longitud
                case ConversionType.MetroAPie: return _client.MetroAPie(value);
                case ConversionType.KilometroAMilla: return _client.KilometroAMilla(value);
                case ConversionType.CentimetroAPulgada: return _client.CentimetroAPulgada(value);
                case ConversionType.PulgadaACentimetro: return _client.PulgadaACentimetro(value);
                case ConversionType.PieAMetro: return _client.PieAMetro(value);
                // Masa
                case ConversionType.KilogramoALibra: return _client.KilogramoALibra(value);
                case ConversionType.GramoAOnza: return _client.GramoAOnza(value);
                case ConversionType.ToneladaAKilogramo: return _client.ToneladaAKilogramo(value);
                case ConversionType.LibraAKilogramo: return _client.LibraAKilogramo(value);
                case ConversionType.OnzaAGramo: return _client.OnzaAGramo(value);
                // Temperatura
                case ConversionType.CelsiusAFahrenheit: return _client.CelsiusAFahrenheit(value);
                case ConversionType.CelsiusAKelvin: return _client.CelsiusAKelvin(value);
                case ConversionType.CelsiusARankine: return _client.CelsiusARankine(value);
                case ConversionType.CelsiusARreaumur: return _client.CelsiusAReaumur(value);
                case ConversionType.FahrenheitACelsius: return _client.FahrenheitACelsius(value);
                default: throw new System.ArgumentOutOfRangeException(nameof(type));
            }
        }

        public void Close()
        {
            try { _client.Close(); } catch { _client.Abort(); }
        }
    }
}

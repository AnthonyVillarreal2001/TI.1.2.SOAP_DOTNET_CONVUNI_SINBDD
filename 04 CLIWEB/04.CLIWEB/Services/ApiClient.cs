using System;
using System.ServiceModel;
using _02.CLIWEB.Models;
using _02.CLIWEB.ConversionServiceReference;

namespace _02.CLIWEB.Services
{
    public class ApiClient : IDisposable
    {
        private readonly ConversionServiceClient _client;

        public ApiClient()
        {
            var url = "http://localhost:57003/ConversionService.svc"; // Cambia si tu WCF usa otro puerto
            var binding = new BasicHttpBinding { MaxReceivedMessageSize = 65536 };
            var address = new EndpointAddress(url);
            _client = new ConversionServiceClient(binding, address);
        }

        public bool ValidateLogin(string user, string pass)
        {
            return _client.Login(user, pass);
        }

        public double Convert(ConversionType t, double v)
        {
            switch (t)
            {
                case ConversionType.MetroAPie: return _client.MetroAPie(v);
                case ConversionType.KilometroAMilla: return _client.KilometroAMilla(v);
                case ConversionType.CentimetroAPulgada: return _client.CentimetroAPulgada(v);
                case ConversionType.PulgadaACentimetro: return _client.PulgadaACentimetro(v);
                case ConversionType.PieAMetro: return _client.PieAMetro(v);
                case ConversionType.KilogramoALibra: return _client.KilogramoALibra(v);
                case ConversionType.GramoAOnza: return _client.GramoAOnza(v);
                case ConversionType.ToneladaAKilogramo: return _client.ToneladaAKilogramo(v);
                case ConversionType.LibraAKilogramo: return _client.LibraAKilogramo(v);
                case ConversionType.OnzaAGramo: return _client.OnzaAGramo(v);
                case ConversionType.CelsiusAFahrenheit: return _client.CelsiusAFahrenheit(v);
                case ConversionType.CelsiusAKelvin: return _client.CelsiusAKelvin(v);
                case ConversionType.CelsiusARankine: return _client.CelsiusARankine(v);
                case ConversionType.CelsiusAReaumur: return _client.CelsiusAReaumur(v);
                case ConversionType.FahrenheitACelsius: return _client.FahrenheitACelsius(v);
                default: throw new ArgumentOutOfRangeException(nameof(t));
            }
        }

        public void Dispose()
        {
            try { _client.Close(); } catch { _client.Abort(); }
        }
    }
}

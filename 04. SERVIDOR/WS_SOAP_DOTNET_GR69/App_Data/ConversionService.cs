using System;

namespace WS_SOAP_DOTNET_GR69.Services
{
    public class ConversionService : IConversionService
    {
        public bool Login(string usuario, string password)
        {
            return (usuario == "MONSTER" && password == "MONSTER9");
        }

        public double MetroAPie(double v) => v * 3.28084;
        public double KilometroAMilla(double v) => v * 0.621371;
        public double CentimetroAPulgada(double v) => v * 0.393701;
        public double PulgadaACentimetro(double v) => v * 2.54;
        public double PieAMetro(double v) => v * 0.3048;

        public double KilogramoALibra(double v) => v * 2.20462;
        public double GramoAOnza(double v) => v * 0.035274;
        public double ToneladaAKilogramo(double v) => v * 1000;
        public double LibraAKilogramo(double v) => v * 0.453592;
        public double OnzaAGramo(double v) => v * 28.3495;

        public double CelsiusAFahrenheit(double v) => (v * 9 / 5) + 32;
        public double CelsiusAKelvin(double v) => v + 273.15;
        public double CelsiusARankine(double v) => (v + 273.15) * 9 / 5;
        public double CelsiusAReaumur(double v) => v * 0.8;
        public double FahrenheitACelsius(double v) => (v - 32) * 5 / 9;
    }
}
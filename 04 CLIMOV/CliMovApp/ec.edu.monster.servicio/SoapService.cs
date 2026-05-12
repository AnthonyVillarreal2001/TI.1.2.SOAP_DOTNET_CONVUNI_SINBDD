using System;
using System.Collections.Generic;
using System.Text;

namespace CliMovApp.ec.edu.monster.servicio
{
    public class SoapService : ISoapService
    {
        public async Task<float> MetroAPieAsync(float v) { await Task.Delay(50); return v * 3.28084f; }
        public async Task<float> KilometroAMillaAsync(float v) { await Task.Delay(50); return v * 0.621371f; }
        public async Task<float> CentimetroAPulgadaAsync(float v) { await Task.Delay(50); return v * 0.393701f; }
        public async Task<float> PulgadaACentimetroAsync(float v) { await Task.Delay(50); return v * 2.54f; }
        public async Task<float> PieAMetroAsync(float v) { await Task.Delay(50); return v * 0.3048f; }

        public async Task<float> CelsiusAFahrenheitAsync(float v) { await Task.Delay(50); return (v * 9f / 5f) + 32f; }
        public async Task<float> CelsiusAKelvinAsync(float v) { await Task.Delay(50); return v + 273.15f; }
        public async Task<float> CelsiusARankineAsync(float v) { await Task.Delay(50); return (v + 273.15f) * 9f / 5f; }
        public async Task<float> CelsiusAReaumurAsync(float v) { await Task.Delay(50); return v * 0.8f; }
        public async Task<float> FahrenheitACelsiusAsync(float v) { await Task.Delay(50); return (v - 32f) * 5f / 9f; }

        public async Task<float> KilogramoALibraAsync(float v) { await Task.Delay(50); return v * 2.20462f; }
        public async Task<float> GramoAOnzaAsync(float v) { await Task.Delay(50); return v * 0.035274f; }
        public async Task<float> ToneladaAKilogramoAsync(float v) { await Task.Delay(50); return v * 1000f; }
        public async Task<float> LibraAKilogramoAsync(float v) { await Task.Delay(50); return v * 0.453592f; }
        public async Task<float> OnzaAGramoAsync(float v) { await Task.Delay(50); return v * 28.3495f; }

        public async Task<bool> ValidarConexionAsync()
        {
            await Task.Delay(10);
            return true;
        }
    }
}

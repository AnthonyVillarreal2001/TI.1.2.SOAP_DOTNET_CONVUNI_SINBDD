using System;
using System.Collections.Generic;
using System.Text;

namespace CliMovApp.ec.edu.monster.servicio
{
    public interface ISoapService
    {
        // Longitud (5)
        Task<float> MetroAPieAsync(float v);
        Task<float> KilometroAMillaAsync(float v);
        Task<float> CentimetroAPulgadaAsync(float v);
        Task<float> PulgadaACentimetroAsync(float v);
        Task<float> PieAMetroAsync(float v);

        // Temperatura (5)
        Task<float> CelsiusAFahrenheitAsync(float v);
        Task<float> CelsiusAKelvinAsync(float v);
        Task<float> CelsiusARankineAsync(float v);
        Task<float> CelsiusAReaumurAsync(float v);
        Task<float> FahrenheitACelsiusAsync(float v);

        // Peso (5)
        Task<float> KilogramoALibraAsync(float v);
        Task<float> GramoAOnzaAsync(float v);
        Task<float> ToneladaAKilogramoAsync(float v);
        Task<float> LibraAKilogramoAsync(float v);
        Task<float> OnzaAGramoAsync(float v);

        Task<bool> ValidarConexionAsync();
    }
}

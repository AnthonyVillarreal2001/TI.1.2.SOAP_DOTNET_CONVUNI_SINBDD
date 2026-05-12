using System;
using System.Collections.Generic;
using System.Text;

namespace CliMovApp.ec.edu.monster.modelo
{
    public class ConversionResult
    {
        private float _valorOriginal;
        private float _valorConvertido;

        private static float Round2(float v) =>
            (float)Math.Round(v, 2, MidpointRounding.AwayFromZero);

        public float ValorOriginal
        {
            get => _valorOriginal;
            set => _valorOriginal = Round2(value);
        }

        public float ValorConvertido
        {
            get => _valorConvertido;
            set => _valorConvertido = Round2(value);
        }

        public string UnidadOrigen { get; set; } = string.Empty;
        public string UnidadDestino { get; set; } = string.Empty;
        public string TipoConversion { get; set; } = string.Empty;
        public DateTime FechaConversion { get; set; }
    }

    public enum TipoConversion { Longitud, Temperatura, Peso }

    public class UnidadConversion
    {
        public string Nombre { get; set; } = string.Empty;
        public string Simbolo { get; set; } = string.Empty;
        public TipoConversion Tipo { get; set; }
    }
}

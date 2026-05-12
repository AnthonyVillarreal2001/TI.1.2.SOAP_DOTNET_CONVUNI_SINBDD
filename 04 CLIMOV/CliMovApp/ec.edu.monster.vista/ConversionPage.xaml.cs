using CliMovApp.ec.edu.monster.servicio;
using System.Globalization;

namespace CliMovApp.ec.edu.monster.vista;

public partial class ConversionPage : ContentPage
{
    private readonly ISoapService _soapService;
    private string _tipoConversionActual = string.Empty;
    private string _unidadOrigen = string.Empty;
    private string _unidadDestino = string.Empty;

    private readonly Dictionary<string, List<string>> _conversionesPorTipo = new()
    {
        { "📏 Longitud", new List<string>
            {
                "Metros → Pies",
                "Kilómetros → Millas",
                "Centímetros → Pulgadas",
                "Pulgadas → Centímetros",
                "Pies → Metros"
            }
        },
        { "🌡️ Temperatura", new List<string>
            {
                "Celsius → Fahrenheit",
                "Celsius → Kelvin",
                "Celsius → Rankine",
                "Celsius → Réaumur",
                "Fahrenheit → Celsius"
            }
        },
        { "⚖️ Peso", new List<string>
            {
                "Kilogramos → Libras",
                "Gramos → Onzas",
                "Toneladas → Kilogramos",
                "Libras → Kilogramos",
                "Onzas → Gramos"
            }
        }
    };

    public ConversionPage()
    {
        InitializeComponent();
        _soapService = new SoapService();
        PickerTipoConversion.SelectedIndex = 0;

        string username = Preferences.Get("username", "Usuario");
        LabelUsuario.Text = $"Bienvenido, {username}";
    }

    private void OnTipoConversionChanged(object sender, EventArgs e)
    {
        if (PickerTipoConversion.SelectedIndex == -1) return;
        _tipoConversionActual = PickerTipoConversion.SelectedItem as string ?? string.Empty;

        if (!_conversionesPorTipo.TryGetValue(_tipoConversionActual, out var conversiones))
            return;

        PickerConversion.ItemsSource = conversiones;
        PickerConversion.SelectedIndex = 0;

        if (conversiones.Count > 0)
        {
            var partes = conversiones[0].Split('→');
            if (partes.Length == 2)
            {
                _unidadOrigen = partes[0].Trim();
                _unidadDestino = partes[1].Trim();
            }
        }

        OcultarResultado();
        EntryValor.Text = string.Empty;
    }

    private void OnConversionChanged(object sender, EventArgs e)
    {
        if (PickerConversion.SelectedItem is not string conversionSeleccionada)
            return;
        var partes = conversionSeleccionada.Split('→');
        if (partes.Length == 2)
        {
            _unidadOrigen = partes[0].Trim();
            _unidadDestino = partes[1].Trim();
        }
    }

    private async void OnConvertirClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(EntryValor.Text))
        {
            await DisplayAlert("⚠️ Error", "Ingrese un valor", "OK");
            return;
        }
        if (!float.TryParse(EntryValor.Text, out float valor))
        {
            await DisplayAlert("⚠️ Error", "Número no válido", "OK");
            return;
        }
        if (PickerConversion.SelectedIndex == -1)
        {
            await DisplayAlert("⚠️ Error", "Seleccione tipo de conversión", "OK");
            return;
        }

        try
        {
            MostrarLoading(true);
            OcultarResultado();
            float resultado = 0;

            switch (_tipoConversionActual)
            {
                case "📏 Longitud":
                    resultado = await ConvertirLongitud(valor, _unidadOrigen, _unidadDestino);
                    break;
                case "🌡️ Temperatura":
                    resultado = await ConvertirTemperatura(valor, _unidadOrigen, _unidadDestino);
                    break;
                case "⚖️ Peso":
                    resultado = await ConvertirPeso(valor, _unidadOrigen, _unidadDestino);
                    break;
            }

            MostrarResultado(resultado, valor, _unidadOrigen, _unidadDestino);
        }
        catch (Exception ex)
        {
            await DisplayAlert("❌ Error", $"Error: {ex.Message}", "OK");
        }
        finally
        {
            MostrarLoading(false);
        }
    }

    private async Task<float> ConvertirLongitud(float valor, string origen, string destino)
    {
        return (origen, destino) switch
        {
            ("Metros", "Pies") => await _soapService.MetroAPieAsync(valor),
            ("Kilómetros", "Millas") => await _soapService.KilometroAMillaAsync(valor),
            ("Centímetros", "Pulgadas") => await _soapService.CentimetroAPulgadaAsync(valor),
            ("Pulgadas", "Centímetros") => await _soapService.PulgadaACentimetroAsync(valor),
            ("Pies", "Metros") => await _soapService.PieAMetroAsync(valor),
            _ => valor
        };
    }

    private async Task<float> ConvertirTemperatura(float valor, string origen, string destino)
    {
        return (origen, destino) switch
        {
            ("Celsius", "Fahrenheit") => await _soapService.CelsiusAFahrenheitAsync(valor),
            ("Celsius", "Kelvin") => await _soapService.CelsiusAKelvinAsync(valor),
            ("Celsius", "Rankine") => await _soapService.CelsiusARankineAsync(valor),
            ("Celsius", "Réaumur") => await _soapService.CelsiusAReaumurAsync(valor),
            ("Fahrenheit", "Celsius") => await _soapService.FahrenheitACelsiusAsync(valor),
            _ => valor
        };
    }

    private async Task<float> ConvertirPeso(float valor, string origen, string destino)
    {
        return (origen, destino) switch
        {
            ("Kilogramos", "Libras") => await _soapService.KilogramoALibraAsync(valor),
            ("Gramos", "Onzas") => await _soapService.GramoAOnzaAsync(valor),
            ("Toneladas", "Kilogramos") => await _soapService.ToneladaAKilogramoAsync(valor),
            ("Libras", "Kilogramos") => await _soapService.LibraAKilogramoAsync(valor),
            ("Onzas", "Gramos") => await _soapService.OnzaAGramoAsync(valor),
            _ => valor
        };
    }

    private void OnLimpiarClicked(object sender, EventArgs e)
    {
        EntryValor.Text = string.Empty;
        OcultarResultado();
        if (PickerConversion.ItemsSource is List<string> list && list.Count > 0)
        {
            PickerConversion.SelectedIndex = 0;
            var partes = list[0].Split('→');
            if (partes.Length == 2)
            {
                _unidadOrigen = partes[0].Trim();
                _unidadDestino = partes[1].Trim();
            }
        }
    }

    private async void OnCerrarSesionClicked(object sender, EventArgs e)
    {
        bool confirmacion = await DisplayAlert("🚪 Cerrar Sesión",
            "¿Está seguro?", "Sí, salir", "Cancelar");
        if (confirmacion)
        {
            Preferences.Remove("isLoggedIn");
            Preferences.Remove("username");
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }

    private void MostrarLoading(bool mostrar)
    {
        LoadingIndicator.IsRunning = mostrar;
        LoadingIndicator.IsVisible = mostrar;
        BtnConvertir.IsEnabled = !mostrar;
        PickerTipoConversion.IsEnabled = !mostrar;
        PickerConversion.IsEnabled = !mostrar;
        EntryValor.IsEnabled = !mostrar;
        BtnConvertir.Text = mostrar ? "CONVIRTIENDO..." : "CONVERTIR";
    }

    private void MostrarResultado(float resultado, float valorOriginal,
                                    string unidadOrigen, string unidadDestino)
    {
        LabelResultado.Text = $"{resultado.ToString("F2", CultureInfo.InvariantCulture)} {unidadDestino}";
        LabelDetalles.Text = $"{valorOriginal.ToString("F2", CultureInfo.InvariantCulture)} {unidadOrigen} = " +
                              $"{resultado.ToString("F2", CultureInfo.InvariantCulture)} {unidadDestino}";
        FrameResultado.IsVisible = true;
    }

    private void OcultarResultado() => FrameResultado.IsVisible = false;

    protected override void OnAppearing()
    {
        base.OnAppearing();
        string username = Preferences.Get("username", "Usuario");
        LabelUsuario.Text = $"Bienvenido, {username}";
    }
}
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;

namespace CliMovApp.ec.edu.monster.vista;

public partial class LoginPage : ContentPage
{
    private const string USERNAME_VALIDO = "MONSTER";
    private const string PASSWORD_VALIDO = "MONSTER09";

    public LoginPage()
    {
        InitializeComponent();
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        string? username = EntryUsuario.Text?.Trim();
        string? password = EntryPassword.Text?.Trim();

        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            await MostrarToast("⚠️ Por favor complete todos los campos");
            return;
        }

        MostrarLoading(true);
        await Task.Delay(800);

        if (username == USERNAME_VALIDO && password == PASSWORD_VALIDO)
        {
            MostrarLoading(false);
            await MostrarToast("✅ Inicio de sesión exitoso");

            Preferences.Set("isLoggedIn", true);
            Preferences.Set("username", username);

            await Task.Delay(500);
            await Shell.Current.GoToAsync("ConversionPage");
            LimpiarCampos();
        }
        else
        {
            MostrarLoading(false);
            await MostrarToast("❌ Usuario o contraseña incorrectos");
            EntryPassword.Text = string.Empty;
        }
    }

    private async Task MostrarToast(string mensaje)
    {
        var toast = Toast.Make(mensaje, ToastDuration.Short, 16);
        await toast.Show();
    }

    private void MostrarLoading(bool mostrar)
    {
        LoadingIndicator.IsRunning = mostrar;
        LoadingIndicator.IsVisible = mostrar;
        BtnLogin.IsEnabled = !mostrar;
        BtnLogin.Text = mostrar ? "INGRESANDO..." : "INGRESAR";
    }

    private void LimpiarCampos()
    {
        EntryUsuario.Text = string.Empty;
        EntryPassword.Text = string.Empty;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        LimpiarCampos();
    }
}
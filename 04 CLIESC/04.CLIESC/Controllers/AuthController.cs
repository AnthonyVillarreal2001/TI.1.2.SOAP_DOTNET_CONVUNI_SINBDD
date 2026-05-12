using _02.CLIESC.Models;
using _02.CLIESC.Views;
using System;
using System.Windows.Forms;

namespace _02.CLIESC.Controllers
{
    public class AuthController
    {
        private readonly LoginForm _view;
        private readonly UserSession _session;

        public AuthController(LoginForm view, UserSession session)
        {
            _view = view; _session = session;

            _view.BtnLogin.Click += (s, e) =>
            {
                var u = _view.TxtUser.Text.Trim();
                var p = _view.TxtPass.Text.Trim();

                var api = new Services.ApiClient();
                bool isAuthenticated = false;

                try
                {
                    isAuthenticated = api.ValidateLogin(u, p);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error de conexión: " + ex.Message);
                    return;
                }

                if (isAuthenticated)
                {
                    _session.SignIn(u);
                    _view.Hide();

                    var conv = new ConvertForm(u);
                    conv.Tag = u;
                    var convCtrl = new ConversionController(conv, _session);

                    conv.FormClosed += (s2, e2) =>
                    {
                        if (!_session.IsAuthenticated)
                        {
                            _view.TxtPass.Clear();
                            _view.Show();
                        }
                        else
                        {
                            _view.Close();
                        }
                    };
                    conv.Show();
                }
                else
                {
                    MessageBox.Show("Usuario o contraseña incorrectos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };
        }
    }
}
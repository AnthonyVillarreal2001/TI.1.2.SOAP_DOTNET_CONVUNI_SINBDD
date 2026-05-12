using System;
using System.Windows.Forms;
using _02.CLIESC.Models;
using _02.CLIESC.Views;
using _02.CLIESC.Controllers;

namespace _02.CLIESC
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var session = new UserSession();
            var login = new LoginForm();
            var auth = new AuthController(login, session);

            Application.Run(login);
        }
    }
}

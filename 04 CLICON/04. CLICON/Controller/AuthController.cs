using _04.CLICON.Models;
using _04.CLICON.Views;
using System;

namespace _04.CLICON.Controllers
{
    public class AuthController
    {
        // Login “quemado”
        private const string USER = "MONSTER";
        private const string PASS = "MONSTER9";

        private readonly ConsoleView _view;
        private readonly UserSession _session;

        public AuthController(ConsoleView view, UserSession session)
        {
            _view = view; _session = session;
        }

        public bool? Login()
        {
            while (true)
            {
                var (u, p) = _view.AskCredentials();
                if (u == USER && p == PASS)
                {
                    _session.SignIn(u);
                    _view.ShowLoginResult(true);
                    return true;
                }

                _view.ShowLoginResult(false);
                Console.WriteLine("\n¿Desea reintentar? [S] para reintentar / [N] para salir");

                var tecla = Console.ReadKey(true).Key;
                if (tecla == ConsoleKey.N)
                {
                    return null; // El usuario eligió salir
                }
            }
        }
    }
}

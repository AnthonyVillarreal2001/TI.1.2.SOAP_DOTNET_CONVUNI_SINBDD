using System;
using _04.CLICON.Controllers;
using _04.CLICON.Models;
using _04.CLICON.Services;
using _04.CLICON.Views;

namespace _04.CLICON
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "04.CLICON - Cliente SOAP (MVC)";

            var view = new ConsoleView();
            var session = new UserSession();
            var auth = new AuthController(view, session);

            // Llamamos al login
            bool? resultadoLogin = auth.Login();

            // Si el resultado es null o false, cerramos el programa
            if (resultadoLogin != true)
            {
                Console.WriteLine("\n\nCerrando aplicación...");
                System.Threading.Thread.Sleep(1000);
                return; // Finaliza el Main y por lo tanto el programa
            }

            // Si llegó aquí, es porque fue true
            var api = new ApiClient();
            var conv = new ConversionController(view, api);

            bool keep = true;
            while (keep)
            {
                keep = conv.MenuLoop(session.Username);
            }

            Console.WriteLine("\n¡Gracias! Presiona una tecla para cerrar...");
            Console.ReadKey();
        }
    }
}

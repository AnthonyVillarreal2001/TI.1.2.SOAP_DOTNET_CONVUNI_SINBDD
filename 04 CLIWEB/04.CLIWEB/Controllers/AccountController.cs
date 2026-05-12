using System.Web.Mvc;
using _02.CLIWEB.Models;
using _02.CLIWEB.Services;   // 👈 Asegúrate de tener este using

namespace _02.CLIWEB.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                ViewBag.Error = "Todos los campos son obligatorios.";
                return View();
            }

            try
            {
                using (var api = new ApiClient())
                {
                    // Llama al método Login del servicio SOAP
                    bool isValid = api.ValidateLogin(username, password);

                    if (isValid)
                    {
                        Session["user"] = new UserSession { Username = username };
                        return RedirectToAction("Index", "Conversion");
                    }
                    else
                    {
                        ViewBag.Error = "Usuario o contraseña incorrectos.";
                    }
                }
            }
            catch (System.ServiceModel.EndpointNotFoundException)
            {
                ViewBag.Error = "No se pudo conectar al servidor. Verifica que el servicio esté activo.";
            }
            catch (System.Exception ex)
            {
                ViewBag.Error = "Error de conexión: " + ex.Message;
            }

            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
using System;
using System.Globalization;
using System.Web.Mvc;
using _02.CLIWEB.Models;
using _02.CLIWEB.Services;

namespace _02.CLIWEB.Controllers
{
    public class ConversionController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Session["user"] == null)
            {
                filterContext.Result = RedirectToAction("Login", "Account");
            }
            base.OnActionExecuting(filterContext);
        }
        private UserSession Current => Session["user"] as UserSession;

        [HttpGet]
        public ActionResult Index()
        {
            if (Current == null || !Current.IsAuthenticated)
                return RedirectToAction("Login", "Account");

            ViewBag.User = Current.Username;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(int type, string value)
        {
            if (Current == null || !Current.IsAuthenticated)
                return RedirectToAction("Login", "Account");

            ViewBag.User = Current.Username;

            if (!double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out double v))
            {
                ViewBag.Error = "Ingrese un valor numérico válido.";
                ViewBag.Result = null; // Limpia resultados previos
                return View();
            }

            var t = (ConversionType)type;
            try
            {
                using (var api = new ApiClient())
                {
                    var res = api.Convert(t, v);
                    ViewBag.Result = res.ToString("F4", CultureInfo.InvariantCulture);
                    ViewBag.ToUnit = GetToUnit(t);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error al consumir el servicio: " + ex.Message;
            }

            return View();
        }

        private static string GetToUnit(ConversionType t)
        {
            switch (t)
            {
                case ConversionType.MetroAPie: return "ft";
                case ConversionType.KilometroAMilla: return "mi";
                case ConversionType.CentimetroAPulgada: return "in";
                case ConversionType.PulgadaACentimetro: return "cm";
                case ConversionType.PieAMetro: return "m";
                case ConversionType.KilogramoALibra: return "lb";
                case ConversionType.GramoAOnza: return "oz";
                case ConversionType.ToneladaAKilogramo: return "kg";
                case ConversionType.LibraAKilogramo: return "kg";
                case ConversionType.OnzaAGramo: return "g";
                case ConversionType.CelsiusAFahrenheit: return "°F";
                case ConversionType.CelsiusAKelvin: return "K";
                case ConversionType.CelsiusARankine: return "°Ra";
                case ConversionType.CelsiusAReaumur: return "°Re";
                case ConversionType.FahrenheitACelsius: return "°C";
                default: return "";
            }
        }
    }
}

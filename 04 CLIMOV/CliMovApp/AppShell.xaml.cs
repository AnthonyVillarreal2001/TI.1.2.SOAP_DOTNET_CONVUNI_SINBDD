using CliMovApp.ec.edu.monster.vista;

namespace CliMovApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("LoginPage", typeof(ec.edu.monster.vista.LoginPage));
            Routing.RegisterRoute("ConversionPage", typeof(ec.edu.monster.vista.ConversionPage));
        }
    }
}

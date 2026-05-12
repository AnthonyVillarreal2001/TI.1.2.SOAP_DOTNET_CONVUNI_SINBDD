namespace _02.CLIESC.Models
{
    public class UserSession
    {
        public bool IsAuthenticated { get; private set; }
        public string Username { get; private set; } = "";
        public void SignIn(string user) { IsAuthenticated = true; Username = user; }
        public void SignOut() { IsAuthenticated = false; Username = ""; }
    }
}

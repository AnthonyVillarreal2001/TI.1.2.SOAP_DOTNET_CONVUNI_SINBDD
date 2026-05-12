namespace _04.CLICON.Models
{
    public class UserSession
    {
        public bool IsAuthenticated { get; private set; }
        public string Username { get; private set; } = "";

        public void SignIn(string user) { IsAuthenticated = true; Username = user; }
        public void SignOut() { IsAuthenticated = false; Username = ""; }
    }
}

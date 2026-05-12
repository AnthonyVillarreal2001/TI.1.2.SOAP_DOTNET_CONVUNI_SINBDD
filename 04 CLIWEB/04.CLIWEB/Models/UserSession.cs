namespace _02.CLIWEB.Models
{
    public class UserSession
    {
        public string Username { get; set; } = "";
        public bool IsAuthenticated => !string.IsNullOrEmpty(Username);
    }
}

namespace ProjektAPI
{
    public class AuthenticationSettings
    {
        public string JwtKey { get; set; }
        public int JwrExpireDays { get; set; }
        public string JwtIssuer { get; set; }
    }
}

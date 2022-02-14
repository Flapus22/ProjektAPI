
using System.ComponentModel.DataAnnotations;

namespace ProjektAPI.Model
{
    public class RegisterUser
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPasswoed { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Nationality { get; set; }
        public DateTime? DateOfBirth { get; set; }
        
    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ProjektAPI.Controllers;
using ProjektAPI.Exceptions;
using ProjektAPI.Model;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProjektAPI.Services
{
    public interface IAccountService
    {
        public void RegisterUser(RegisterUser registerUser);
        string GenerateJwt(Login login);
    }
    public class AccountService : IAccountService
    {
        private readonly WeatherDataContext db;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly AuthenticationSettings _authenticationSettings;

        public BinaryReader ClaimType { get; private set; }

        public AccountService(WeatherDataContext db, IPasswordHasher<User> passwordHasher, AuthenticationSettings authenticationSettings)
        {
            this.db = db;
            _passwordHasher = passwordHasher;
            _authenticationSettings = authenticationSettings;
        }


        public void RegisterUser(RegisterUser registerUser)
        {
            var newUser = new User
            {
                Email = registerUser.Email,
                FirstName = registerUser.FirstName,
                LastName = registerUser.LastName,
                Nationality = registerUser.Nationality,
                DateOfBirth = registerUser.DateOfBirth
            };
            var hashedPassword = _passwordHasher.HashPassword(newUser, registerUser.Password);
            newUser.PasswordHash = hashedPassword;

            db.Users.Add(newUser);
            db.SaveChanges();
        }

        public string GenerateJwt(Login login)
        {
            var user = db.Users.FirstOrDefault(x=>x.Email == login.Email);

            if(user == null)
            {
                throw new BadRequestException("Zły Email albo Hasło");
            }

            var result = _passwordHasher.VerifyHashedPassword(user,user.PasswordHash,login.Password);

            if(result == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Zły Email albo Hasło");
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,user.IdUser.ToString()),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                new Claim("DateOfBirth",user.DateOfBirth.Value.ToString("yyyy-MM-dd")),
            };

            if(!string.IsNullOrEmpty(user.Nationality))
                claims.Add(new Claim("Nationality",user.Nationality));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));

            var cred = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_authenticationSettings.JwrExpireDays);

            var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer, _authenticationSettings.JwtIssuer, claims, expires: expires, signingCredentials: cred);

            var tokenHandler = new JwtSecurityTokenHandler();

            return tokenHandler.WriteToken(token);
        }

    }
}

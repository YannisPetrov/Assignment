namespace Assignment.Services
{
    using Assignment.Models;
    using Microsoft.IdentityModel.Tokens;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Security.Principal;

    public class UserDataService : IUserDataService
    {
        private readonly IConfiguration _configuration;

        public UserDataService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(User user)
        {
            //A list of claims is created so that
            //The user's name will be a parameter
            //Of the token
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName)
            };

            //We create the security key that will be used as a token parameter
            var securityKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            //And then the credentials parameter
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //We create the token
            var token = new JwtSecurityToken(
                signingCredentials: credentials,
                issuer: "Sample",
                audience: "Sample",
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1));

            //And we assign a new token handler
            var handler = new JwtSecurityTokenHandler();

            //Then we return the token by the handler
            return handler.WriteToken(token);
        }

        public TokenValidationParameters GetValidationParameters()
        {
            return new TokenValidationParameters()
            {
                ValidateLifetime = false, // Because there is no expiration in the generated token
                ValidateAudience = false, // Because there is no audiance in the generated token
                ValidateIssuer = false,   // Because there is no issuer in the generated token
                ValidIssuer = "Sample",
                ValidAudience = "Sample",
                IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value)) // The same key as the one that generate the token
            };
        }

        public User SeededUserData()
        {
            //The hardcoded username and password
            var validUser = "validUserName";
            var validPass = "validUserPassword";

            //Returned in the form of an Object from the User class
            return new User
            {
                UserName = validUser,
                Password = validPass
            };
        }

        public bool ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var validationParameters = GetValidationParameters();

            SecurityToken validatedToken;

            IPrincipal principal = tokenHandler.ValidateToken(token, validationParameters, out validatedToken);
            return true;
        }

    }
}

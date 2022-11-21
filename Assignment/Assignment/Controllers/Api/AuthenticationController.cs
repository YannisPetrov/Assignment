namespace Assignment.Controllers.Api
{
    using Assignment.Services;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/login")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserDataService userData;

        public AuthenticationController(IConfiguration configuration, IUserDataService userData)
        {
            _configuration = configuration;
            this.userData = userData;
        }

        //Here we specify that the method below will return data to the given URL
        [HttpGet("token")]
        public bool GetTokenValidation()
        {
            //We validate the token and store the value in a bool variable
            bool validatedToken = userData.ValidateToken(Globals.token);

            //Then it is returned
            return validatedToken;
        }
    }
}
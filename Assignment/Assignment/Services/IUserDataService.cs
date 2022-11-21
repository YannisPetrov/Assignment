namespace Assignment.Services
{
    using Assignment.Models;
    using Microsoft.IdentityModel.Tokens;

    public interface IUserDataService
    {
        //This method will be used to hardcode the user login information
        User SeededUserData();
        
        //This will generate the token
        string GenerateToken(User user);

        //This method will get the validation parameters, so the method below works properly
        TokenValidationParameters GetValidationParameters();

        //This method will validate the created token
        bool ValidateToken(string token);

        
        
    }
}

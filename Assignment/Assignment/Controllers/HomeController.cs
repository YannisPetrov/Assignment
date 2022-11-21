namespace Assignment.Controllers
{
    using Assignment.Models;
    using Assignment.Models.DTOs;
    using Assignment.Services;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : Controller
    {

        private readonly IUserDataService userData;

        public HomeController(IUserDataService userData)
        {
            this.userData = userData;
        }

        //A string variable to hold the error massage
        string error = string.Empty;
        
        //This loads the Index page by returning a "View"
        public IActionResult Index() => View();

        //Here we create a method by which we access the hardcoded user data
        public User GetUserData()
        => this.userData.SeededUserData();

        //Here the user input is posted to the method below
        //And it accepts the UserDto object as a parameter
        [HttpPost]
        public IActionResult Index(UserDto user)
        {
            //We check if the user input corresponds to the hardocded data

            //First we check the username
            if (GetUserData().UserName != user.UserName)
            {
                error = "Wrong Username.";

                return Ok(error);
            }
            //And here the password
            if (GetUserData().Password != user.Password)
            {
                error = "Wrong Password.";

                return Ok(error);
            }

            //If the user input corresponds with the hardcoded data
            //A token is created
            Globals.token = userData.GenerateToken(GetUserData());

            return View();
        }
    }
}
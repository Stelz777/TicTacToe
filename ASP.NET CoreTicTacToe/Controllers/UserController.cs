using ASP.NETCoreTicTacToe.Models;
using ASP.NETCoreTicTacToe.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace ASP.NETCoreTicTacToe.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Authenticate(User userParam)
        {
            if (userParam == null)
            {
                return BadRequest();
            }

            var user = userService.Authenticate(userParam.Name, userParam.Password);
            if (user == null)
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }
            return Ok(user);
        }

        [HttpGet]
        public IActionResult All()
        {
            var users = userService.GetAll();
            return Ok(users);
        }
    }
}
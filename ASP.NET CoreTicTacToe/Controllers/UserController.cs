﻿using ASP.NETCoreTicTacToe.Infrastructure;
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

        private UserAPI userAPI;

        public UserController(UserAPI userAPI, IUserService userService)
        {
            this.userAPI = userAPI;
            this.userService = userService;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Authenticate([FromBody]User userParam)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }

            if (userParam == null)
            {
                return BadRequest();
            }

            var user = userService.Authenticate(userAPI, userParam.Name, userParam.Password);
            if (user == null)
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }
            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Register(User userParam)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "User already exists" });
            }
            if (userParam == null)
            {
                return BadRequest();
            }
            var user = userService.Register(
                userAPI, userParam.Name, userParam.Password, userParam.FirstName, userParam.LastName);
            if (user == null)
            {
                return BadRequest(new { message = "User already exists" });
            }
            return Ok(user);
        }

        [HttpGet]
        public IActionResult All()
        {
            var users = userService.GetAll(userAPI);
            return Ok(users);
        }
    }
}
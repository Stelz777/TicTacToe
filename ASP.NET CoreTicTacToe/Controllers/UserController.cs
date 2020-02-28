﻿using ASP.NETCoreTicTacToe.Infrastructure;
using ASP.NETCoreTicTacToe.Infrastructure.Services;
using ASP.NETCoreTicTacToe.Models;
using ASP.NETCoreTicTacToe.Models.Users;
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
        public IActionResult Authenticate(UserForLogin userParam)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }

            if (userParam == null)
            {
                return BadRequest();
            }

            var user = userService.Authenticate(userParam);
            if (user == null)
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }
            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Register(UserForRegistration userParam)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "User already exists" });
            }
            if (userParam == null)
            {
                return BadRequest();
            }
            var user = userService.Register(userParam);
            if (user == null)
            {
                return BadRequest(new { message = "User already exists" });
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
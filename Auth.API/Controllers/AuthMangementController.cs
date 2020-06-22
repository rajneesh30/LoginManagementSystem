using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Auth.API.Helper;
using Auth.API.Model;
using Auth.API.Services;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;

namespace Auth.API.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/AuthManager")]
    [ApiController]
    public class AuthMangementController : ControllerBase
    {
        private IAuthProviderService _authService;
        private IMapper _mapper;
        //private readonly ILogger<AuthMangementController> _logger;

        /// <summary>
        /// Controller constructor to initialize services
        /// </summary>
        /// <param name="authService"></param>
        /// <param name="mapper"></param>
        public AuthMangementController(
           IAuthProviderService authService,
           IMapper mapper
           )
        {
            _authService = authService;
            _mapper = mapper;
            
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] Login model)
        {
            var user = _authService.LoginUser(model);

            //_logger.LogInformation("Hello, {Name}!", model.Email);
            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });           

            // return basic user info 
            return Ok(new
            {
                Id = user.Email               
            });
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] Login model)
        {          
            
            try
            {
                //_logger.LogInformation("Inside Registration api, {Name}!", model.Email);
                // create user
                _authService.RegisterUser(model);
                return Ok();
            }
            catch (AppException ex)
            {
                //_logger.LogInformation("Registration had problem for: , {Name}!", model.Email);
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("getall")]
        public async  Task<IActionResult> GetAllUserDetails()
        {
            var users = _authService.AllLoginDetailsAsync().Result;
            //var model = _mapper.Map<List<LoginDetails>>(users.Result);
            //_logger.LogInformation("SuccesfullyFetched record for LoginDetails");
            return Ok(users);
        }
    }
}

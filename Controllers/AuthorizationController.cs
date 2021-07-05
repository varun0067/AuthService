using AuthorizationMS.DTO;
using AuthorizationMS.Interface;
using AuthorizationMS.Models;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AuthorizationMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly IAccountService _service;
        private readonly ILog _log4net = LogManager.GetLogger(typeof(AuthorizationController));

        public AuthorizationController(IAccountService service)
        {
            _service = service;
        }

        [HttpGet("accounts")]
        public ActionResult GetAllAccounts()
        {
            return Ok(_service.GetAccounts());
        }
        
        [HttpGet("accounts/{customerId}")]
        public async Task<IActionResult> GetCustomer(string customerId)
        {
            try
            {
                _log4net.Info("Getting customer details on id "+customerId);

                var user = await _service.GetUserById(customerId);

                if (user != null)
                {
                    _log4net.Info("Returning user details " + user.Name);
                    return Ok(user);
                }
                else
                {
                    _log4net.Error("User not found on id " + customerId);
                    return BadRequest("User Not Found");
                }
            }
            catch(Exception ex)
            {
                _log4net.Error("Something went wrong. " + ex.Message);
                return StatusCode(500,"Internal Sever error."+ex.Message);
            }
        }

        [HttpGet("usernames")]
        public async Task<IActionResult> GetUsernames()
        {
            _log4net.Info("Getting all usernames.");
            try
            {
                _log4net.Info("Returning usernames array.");
               return Ok(await _service.GetUsernames());
            }
            catch(Exception ex)
            {
                _log4net.Error("Something went wrong. " + ex.Message);
                return StatusCode(500, "Internal Sever error. " + ex.Message);
            }
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserDTO user)
        {
            _log4net.Info("Checking login.");
            try
            {
                bool LoginSuccess = await _service.LoginCheck(user);

                if (LoginSuccess)
                {
                    _log4net.Info("Login success");
                    return Ok(await _service.CreateJwt(user));
                }
                else
                {
                    _log4net.Error("Login falied invalid credentials");
                    return BadRequest("Invalid credentials!");
                }
            }
            catch(Exception ex)
            {
                _log4net.Error("Something went wrong. " + ex.Message);
                return StatusCode(500, "Internal Sever error. " + ex.Message);
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]Account account)
        {
            _log4net.Info("Registering user.");
            try
            {
                await _service.RegisterUser(account);
                _log4net.Info("Registered successfully");
                return Ok(true);
            }
            catch(Exception ex)
            {
                _log4net.Error("Something went wrong. " + ex.Message);
                return StatusCode(500, "Internal Sever error. " + ex.Message);
            }          
        }

        [Authorize]
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateDTO account)
        {
            _log4net.Info("Updating user.");
            try
            {
                string reponse = await _service.UpdateUser(account);

                if (reponse == "details Updated successfully")
                {
                    _log4net.Info(reponse);
                    return Ok(true);
                }
                else
                {
                    _log4net.Error(reponse);
                    return BadRequest(reponse);
                }
            }
            catch(Exception ex)
            {
                _log4net.Error("Something went wrong. " + ex.Message);
                return StatusCode(500, "Internal Sever error. " + ex.Message);
            }
        }

        [HttpPut("forgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody]ForgotPasswordDTO forgotPassword)
        {
            _log4net.Info("Forgot password -- updating new password");
            try
            {
                bool passwordChanged = await _service.ForgotPassword(forgotPassword);
                if (passwordChanged)
                {
                    _log4net.Info("Password changed -- forogt password");
                    return Ok(true);
                }
                else
                {
                    _log4net.Error("Password not changed -- forgot password -- invalid credentials");
                    return BadRequest("Could'nt update password");
                }
            }
            catch(Exception ex)
            {
                _log4net.Error("Something went wrong. " + ex.Message);
                return StatusCode(500, "Internal Sever error. " + ex.Message);
            }
        }

        [Authorize]
        [HttpPut("changePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDTO changePassword)
        {
            _log4net.Info("Change password -- updating new password");
            try
            {
                bool passwordChanged = await _service.ChangePassword(changePassword);
                if (passwordChanged)
                {
                    
                    return Ok(true);
                }
                else
                {
                    _log4net.Error("Password not changed -- change password -- invalid credentials");
                    return BadRequest("Could'nt update password");
                }
                    
            }
            catch(Exception ex)
            {
                _log4net.Error("Something went wrong. " + ex.Message);
                return StatusCode(500, "Internal Sever error. " + ex.Message);
            }
        }

    }
}

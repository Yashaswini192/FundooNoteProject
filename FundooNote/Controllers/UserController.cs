using BusinessLayer.Interface;
using BusinessLayer.Service;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepoLayer.Entity;
using System;
using System.Linq;
using System.Security.Claims;

namespace FundooNote.Controllers
{
    [Route("api/[controller]")] 
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBusiness userBusiness;

        public UserController(IUserBusiness userBusiness)
        {
            this.userBusiness = userBusiness;
        }

        [HttpPost] //inserting values
        [Route("Registration")]

        public IActionResult UserRegister(UserRegistration userReg)
        {
            try
            {
                var result = userBusiness.Registration(userReg);

                if (result != null)
                {
                    return Ok(new { success = true, message = "Registration Success", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Registration Not Successfull" });
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpPost]
        [Route("UserLogin")]
        public IActionResult UserLogin(Login login)
        {
            try
            {
                var result = userBusiness.UserLogin(login);

                if (result != null)
                {
                    return Ok(new { success = true, message = "Login Successfull", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Login unsuccessfull" });
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpPost]
        [Route("ForgotPassword")]

        public IActionResult ForgotPassword(string email)
        {
            try
            {
                var result = userBusiness.ForgotPassword(email);

                if (result != null)
                {
                    return Ok(new { success = true, message = "Reset Link is sent to your email"});
                }
                else
                {
                    return BadRequest(new { success = false, message = "Email not found" });
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }



        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut]
        [Route("ResetPassword")]
        public IActionResult ResetPassword(ResetPasswordModel resetPassword)
        {
            try
            {
                var email =User.Claims.FirstOrDefault(x=>x.Type=="Email").Value.ToString();
                var result = userBusiness.ResetPassword(resetPassword, email);

                if (result != false)
                {
                    return Ok(new { success = true, message = "Password has Changed successfully", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Password has not changed" });
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }

        [HttpGet]
        [Route("GetAllUsers")]
        public IActionResult GetAllUsers()
        {
            try
            {
                var res = userBusiness.GetALLUsers();
                if(res != null)
                {
                    return Ok(new {success = true , message = "Retreived All Users " , data = res});
                }
                else
                {
                    return BadRequest(new { success = false, message = "Failed to Retreive Data" });
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("GetUserByUserId")]
        public IActionResult GetUserByUserId(long UserId)
        { 
            try
            {
                var res = userBusiness.GetUserByUserId(UserId);
                if(res != null)
                {
                    return Ok(new { success = true , message = "Retreived All users by user ID ",data = res});
                }
                else
                {
                    return BadRequest(new { success = false, message = "User Id is not Found" });
                }
            }
            catch(Exception ex) 
            {
                throw ex;
            }
        }


        }
}

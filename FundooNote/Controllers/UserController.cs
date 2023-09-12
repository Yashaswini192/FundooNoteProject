using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
            var result = userBusiness.Registration(userReg);

            if (result != null)
            {
                return Ok(new {success = true,message = "Registration Success",data = result});
            }
            else
            {
                return BadRequest(new {success = false,message = "Registration Not Successfull"});
            }
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult UserLogin(Login login)
        {
            var result = userBusiness.UserLogin(login);

            if(result != null)
            {
                return Ok(new { success = true, message = "Login Successfull", data = result });
            }
            else
            {
                return BadRequest(new {success = false,message = "Login unsuccessfull" });
            }
        }
    }
}

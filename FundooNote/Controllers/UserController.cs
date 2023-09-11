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

        [HttpPost]
        [Route("Registration")]

        public IActionResult UserRegister(UserRegistration userReg)
        {
            var result = userBusiness.Registration(userReg);

            if (result != null)
            {
                return Ok(new {message = "Registration Success",data = result});
            }
            else
            {
                return BadRequest(new {message = "Registration Not Successfull",data = result});
            }
        }
    }
}

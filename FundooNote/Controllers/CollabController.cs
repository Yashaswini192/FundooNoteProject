using BusinessLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace FundooNote.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollabController : ControllerBase
    { 
        private readonly ICollabBusiness collabBusiness;
        public CollabController(ICollabBusiness collabBusiness)
        {
            this.collabBusiness = collabBusiness;
        }

        [Authorize,HttpPost]
        [Route("CreateCollab")]

        public IActionResult CreateCollab(string Email,int NoteId)
        {
            try 
            {
                int userId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == "UserID").Value);
                var result = collabBusiness.CreateCollab(Email, userId, NoteId);
                if(result != null)
                {
                    return Ok(new {success = true,message = "SuccessFully collaborated",data = result});
                }
                else
                {
                    return BadRequest(new { success = false, message = "CouldNot Collaborate" });
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

    }
}

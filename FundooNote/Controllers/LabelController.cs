using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepoLayer.Entity;
using System;
using System.Linq;
using System.Reflection.Emit;
using System.Threading.Tasks;

namespace FundooNote.Controllers 
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabelController : ControllerBase
    {
        private readonly ILabelBusiness labelBusiness;
        public LabelController(ILabelBusiness labelBusiness)
        {
            this.labelBusiness = labelBusiness;
        }

        [Authorize,HttpPost]
        [Route("CreateLabel")]
        public IActionResult CreateLabel(LabelModel labelmodel, int UserId, int NoteId)
        {
            try
            {
                int userId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == "UserID").Value);
                var result = labelBusiness.CreateLabel(labelmodel, UserId, NoteId);
                if(result != null)
                {
                    return Ok(new { success = true, message = "Label Created SuccessFully", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Label is not created" });
                }

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [Authorize,HttpGet]
        [Route("RetreiveLabel")]

        public IActionResult RetreiveLabel(int UserId)
        {
            try
            {
                int userId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == "UserID").Value);
                var res = labelBusiness.RetrieveLabel(UserId);
                if(res != null)
                {
                    return Ok(new { success = true, message = "Retreived Successfully", data = res });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Could not Find Label" });
                }

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [Authorize,HttpPost]
        [Route("UpdateLabel")]

        public IActionResult UpdateLabel(string newLabelName, long UserId, string labelName)
        {
            try
            {
                var result = labelBusiness.UpdateLabel(newLabelName, UserId, labelName);
                if(result != null)
                {
                    return Ok(new { success = true,message = "Label Updated Successfully",data = result});
                }
                else
                {
                    return BadRequest(new {success = false,message = "Could not update label"});
                }

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [Authorize, HttpGet]
        [Route("DeleteLabel")]
        public IActionResult DeleteLabel(string labelName, int userId)
        {
            try
            {
                var result = labelBusiness.DeleteLabel(labelName, userId);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Label Deleted Successfully" });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Could not Found label" });
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RepoLayer.Context;
using RepoLayer.Entity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNote.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly INoteBusiness noteBusiness;

        public NoteController(INoteBusiness noteBusiness)
        {
            this.noteBusiness = noteBusiness;
        }

        [Authorize]
        [HttpPost]
        [Route("NoteCreation")]
        public IActionResult NoteCreation(CreateNoteModel createNote)
        {
            try
            {
                int userId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == "UserID").Value);
                var result = noteBusiness.CreateNote(createNote, userId);

                if(result != null)
                {
                    return Ok(new { success = true, message = "Note Created Successfully", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Note is not created" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [Authorize]
        [HttpGet]
        [Route("RetreiveNote")]

        public IActionResult RetreiveNote(int NoteId)
        {
            try
            {
                var result = noteBusiness.RetreiveNote(NoteId);

                if(result != null)
                {
                    return Ok(new { success = true, message = "Retreived Note Successfully", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "CouldNot find NoteID" });
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpPost]
        [Route("UpdateNote")]

        public IActionResult UpdateNote(CreateNoteModel createNote, int NoteId, int userId)
        {
            try
            {
                var result = noteBusiness.UpdateNote(createNote, NoteId, userId);
                if (result != null)
                {
                    return Ok(new { success = true,data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Note is not Updated"}) ;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpGet]
        [Route("DeleteNote")]
        public IActionResult DeleteNote(int NoteId)
        {
            try
            {
                var result = noteBusiness.DeleteNote(NoteId);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Note Deleted SuccessFully" });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Note is Not Deleted" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpPost]
        [Route("UploadImage")]
        public async Task<IActionResult> UploadImage(int NoteId, IFormFile imageFile)
        {
            try
            {
                int userId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == "UserID").Value);
                Tuple<int, string> result = await noteBusiness.Image(NoteId, imageFile, userId);
                if (result.Item1 == 1)
                {
                    return Ok(new { success = true, messege = "Image Update Sucessfully", data = result });
                }
                else
                {
                    return NotFound(new { success = false, messege = "Image is not Uploaded" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [Authorize, HttpPost]
        [Route("IsTrash")]
        public IActionResult IsTrash(int NoteId)
        {
            try
            {
                var result = noteBusiness.IsTrash(NoteId);
                if (result == true)
                {
                    return Ok(new { success = true, message = "Note Trashed SuccessFully", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Note UnTrashed SuccessFully", data = result });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}


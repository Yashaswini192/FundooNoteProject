using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

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

        [HttpPost]
        [Route("NoteCreation")]
        public IActionResult NoteCreation(CreateNoteModel createNote,int UserId)
        {
            try
            {
                var result = noteBusiness.CreateNote(createNote,UserId);

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

       
    }
}

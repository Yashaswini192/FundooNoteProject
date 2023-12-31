﻿using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RepoLayer.Context;
using RepoLayer.Entity;
using System;
using System.Collections.Generic;
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
                int userId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == "UserID").Value);
                var result = noteBusiness.RetreiveNote(NoteId,userId);

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
                int userId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == "UserID").Value);
                var result = noteBusiness.DeleteNote(NoteId, userId);
                if (result != false)
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


        [Authorize, HttpPut] 
        [Route("IsTrash")]
        public IActionResult IsTrash(int NoteId)
        {
            try
            {
                int userId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == "UserID").Value);
                var result = noteBusiness.IsTrash(NoteId, userId);
                if (result != null)
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

        [Authorize, HttpGet]
        [Route("GetALLNotes")]

        public IActionResult GetALLNotes()
        {
            try
            {
                int userId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == "UserID").Value);
                var result = noteBusiness.GetALLNotes(userId);
                if(result != null)
                {
                    return Ok(new { success = true, message = "Retreived ALL Notes SuccessFully", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Failed to Retreive Notes" });
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [Authorize, HttpPut]
        [Route("ISArchive")]

        public IActionResult ISArchive(int NoteId)
        {
            try
            {
                int userId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == "UserID").Value);
                var result = noteBusiness.IsArchive(NoteId, userId);
                if(result != null)
                {
                    return Ok(new { success = true, message = "Note Archived Successfully", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Note cannot be unarchived." });
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [Authorize, HttpPut]
        [Route("ISPin")]

        public IActionResult ISPin(int NoteId)
        {
            try
            {
                int userId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == "UserID").Value);
                var result = noteBusiness.IsPin(NoteId,userId);
                if(result == true)
                {
                    return Ok(new { success = true, message = "Note is Pinned SuccessFully", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Note is UnPinned SuccessFully" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [Authorize,HttpPost]
        [Route("NoteColor")]

        public IActionResult NoteColor(int NoteId, string Color)
        {
            try
            {
                var result = noteBusiness.Color(NoteId,Color);
                if(result != null)
                {
                    return Ok(new { success = true, message = "SuccessFully Added Color to Note", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Could Not Add Color to Note" });
                }

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [Authorize,HttpGet]
        [Route("SearchQuery")]

        public IActionResult SearchQuery(string keyword)
        {
            try
            {
                var result = noteBusiness.SearchQuery(keyword);
                if(result != null)
                {
                    return Ok(new { success = true, message = "SuccessFully Retreived Note With Given Keyword", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "CouldNot Find Note with Given Keyword" });
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}


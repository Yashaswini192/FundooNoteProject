using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using RepoLayer.Entity;
using RepoLayer.Interface;
using RepoLayer.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service
{
    public class NoteBusiness : INoteBusiness
    {
        private readonly INotesRepo notesRepo;

        public NoteBusiness(INotesRepo notesRepo)
        {
            this.notesRepo = notesRepo;
        }
        public Notes CreateNote(CreateNoteModel createNote,int UserId)
        {
            try
            {
                return notesRepo.CreateNote(createNote,UserId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Notes RetreiveNote(int NoteId, int userId)
        {
            try
            {
                return notesRepo.RetreiveNote(NoteId, userId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Notes UpdateNote(CreateNoteModel createNote, int NoteId, int userId)
        {
            try
            {
                return notesRepo.UpdateNote(createNote,NoteId,userId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteNote(int NoteId, int userId)
        {
            try
            {
                return notesRepo.DeleteNote(NoteId, userId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
        public async Task<Tuple<int, string>> Image(int noteId, IFormFile imageFile, int userId)
        {
            try
            {
                return await notesRepo.Image(noteId, imageFile, userId);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public Notes IsTrash(int NoteId, int userId)
        {
            try
            {
                return  notesRepo.IsTrash(NoteId,userId);
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public List<Notes> GetALLNotes(int userId)
        {
            try
            {
                return notesRepo.GetALLNotes(userId);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public bool IsArchive(int NoteId,int userId)
        {
            try
            {
                return notesRepo.IsArchive(NoteId,userId);
            }
            catch(Exception ex)
            {
                throw (ex);
            }
        }

        public bool IsPin(int NoteId, int userId)
        {
            try
            {
                return notesRepo.IsPin(NoteId,userId);
            }
            catch(Exception ex)
            {
                throw (ex);
            }
        }

        public Notes Color(int NoteId, string Color)
        {
            try
            {
                return notesRepo.Color(NoteId, Color);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public List<Notes> SearchQuery(string keyword)
        {
            try
            {
                return notesRepo.SearchQuery(keyword);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }


    }
}

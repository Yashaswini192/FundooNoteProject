using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Identity;
using RepoLayer.Entity;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

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
                return null;
            }
        }
    }
}

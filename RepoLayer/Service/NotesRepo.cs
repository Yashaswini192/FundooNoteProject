using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using RepoLayer.Context;
using RepoLayer.Entity;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepoLayer.Service
{
    public class NotesRepo : INotesRepo
    {
        private readonly FundooContext fundooContext;
        private readonly IConfiguration configuration;

        public NotesRepo(FundooContext fundooContext, IConfiguration configuration)
        {
            this.fundooContext = fundooContext;
            this.configuration = configuration;
        }

        public Notes CreateNote(CreateNoteModel createNote,int UserId)
        {
            try
            {
                Notes notes = new Notes();

                notes.Title = createNote.Title;
                notes.Description = createNote.Description;
                notes.Remainder = createNote.Remainder;
                notes.BgColor = createNote.BgColor;
                notes.Image = createNote.Image;
                notes.Archive = createNote.Archive;
                notes.PinNote = createNote.PinNote;
                notes.Trash = createNote.Trash;
                notes.UserId = UserId;

                fundooContext.Notes.Add(notes);
                fundooContext.SaveChanges();
                
                if(notes != null)
                {
                    return notes;
                }
                else
                {
                    return null;
                }

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }


        public Notes RetreiveNote(int NoteId)
        {
            try
            {
                var result = fundooContext.Notes.FirstOrDefault(x => x.NoteId == NoteId);

                if(result != null)
                {
                    return result;
                }
                else
                {
                    return null;
                }

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}

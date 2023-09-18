using CommonLayer.Model;
using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepoLayer.Interface
{
    public interface INotesRepo
    {
        public Notes CreateNote(CreateNoteModel createNote,int UserId);
    }
}

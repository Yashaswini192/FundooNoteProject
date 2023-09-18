using CommonLayer.Model;
using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface INoteBusiness
    {
        public Notes CreateNote(CreateNoteModel createNote,int UserId);

        public Notes RetreiveNote(int NoteId);

    }
}

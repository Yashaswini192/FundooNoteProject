using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepoLayer.Interface
{
    public interface INotesRepo
    {
        public Notes CreateNote(CreateNoteModel createNote,int UserId);

        public Notes RetreiveNote(int NoteId);

        public Notes UpdateNote(CreateNoteModel createNote, int NoteId, int userId);

        public bool DeleteNote(int NoteId);

        public  Task<Tuple<int, string>> Image(int noteId, IFormFile imageFile,int userId);

        public bool IsTrash(int NoteId);
    }
}

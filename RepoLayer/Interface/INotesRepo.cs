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

        public Notes RetreiveNote(int NoteId, int userId);

        public Notes UpdateNote(CreateNoteModel createNote, int NoteId, int userId);

        public bool DeleteNote(int NoteId, int userId);

        public  Task<Tuple<int, string>> Image(int noteId, IFormFile imageFile,int userId);

        public  Notes IsTrash(int NoteId, int userId);

        public List<Notes> GetALLNotes(int userId);

        public bool IsArchive(int NoteId, int userId);

        public bool IsPin(int NoteId, int userId);

        public Notes Color(int NoteId, string Color);

        public List<Notes> SearchQuery(string keyword);
    }
}

﻿using CommonLayer.Model;
using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepoLayer.Interface
{
    public interface INotesRepo
    {
        public Notes CreateNote(CreateNoteModel createNote,int UserId);

        public Notes RetreiveNote(int NoteId);

        public Notes UpdateNote(CreateNoteModel createNote, int NoteId, int userId);

        public bool DeleteNote(int NoteId);

    }
}

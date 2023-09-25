using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface ICollabBusiness
    {
        public CollabEntity CreateCollab(string Email, int UserId, int NoteId);

        public bool DeleteCollab(int collabId);
    }
}

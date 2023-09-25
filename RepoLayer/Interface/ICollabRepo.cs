using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepoLayer.Interface
{
    public interface ICollabRepo
    {
        public CollabEntity CreateCollab(string Email, int UserId, int NoteId);

        public bool DeleteCollab(int collabId);
        public List<CollabEntity> RetreiveAll();
    }
}

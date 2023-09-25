using BusinessLayer.Interface;
using RepoLayer.Entity;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class CollabBusiness : ICollabBusiness
    {
        private readonly ICollabRepo collabRepo;

        public CollabBusiness(ICollabRepo collabRepo)
        {
            this.collabRepo = collabRepo;
        }
        public CollabEntity CreateCollab(string Email, int UserId, int NoteId)
        {
            try
            {
                return collabRepo.CreateCollab(Email, UserId, NoteId);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteCollab(int collabId)
        {
            try
            {
                return collabRepo.DeleteCollab(collabId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CollabEntity> RetreiveAll()
        {
            try
            {
                return collabRepo.RetreiveAll();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

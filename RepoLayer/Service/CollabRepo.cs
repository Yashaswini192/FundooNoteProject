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
    public class CollabRepo : ICollabRepo
    {
        private readonly FundooContext fundooContext;
        private readonly IConfiguration configuration;

        public CollabRepo(FundooContext fundooContext,IConfiguration configuration)
        {
            this.fundooContext = fundooContext;
            this.configuration = configuration;
        }

        public CollabEntity CreateCollab(string Email,int UserId,int NoteId)
        {
            try
            {
                CollabEntity collab = new CollabEntity();
                collab.Email = Email;
                collab.UserId = UserId;
                collab.NoteId = NoteId;
                fundooContext.Add(collab);
                fundooContext.SaveChanges();
                return collab;
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
                var collabid = fundooContext.Collab.Where(x  => x.CollabId == collabId).FirstOrDefault();
                if (collabid != null)
                {
                    fundooContext.Collab.Remove(collabid);
                    fundooContext.SaveChanges();
                    return true;
                }
                return false;               
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public CollabEntity RetreiveAll(int userId, int NoteId)
        {
            try
            {
                var res = fundooContext.Collab.Where(x =>x.UserId == userId && x.NoteId == NoteId).FirstOrDefault();
                if(res != null)
                {
                    return res;
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

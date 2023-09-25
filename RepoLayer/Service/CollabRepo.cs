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

    }
}

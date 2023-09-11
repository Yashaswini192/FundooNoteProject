using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using RepoLayer.Context;
using RepoLayer.Entity;
using RepoLayer.Interface;
using RepoLayer.Migrations;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepoLayer.Service
{
    public class UserRepo : IUserRepo
    {
        private readonly FundooContext fundoo;

        private readonly IConfiguration configuration;

        public UserRepo(FundooContext fundoo, IConfiguration configuration)
        {
            this.fundoo = fundoo;
            this.configuration = configuration;

        }

        public User Registration(UserRegistration userRegistration)
        {
            User user = new User();

            user.FirstName = userRegistration.FirstName;
            user.LastName = userRegistration.LastName;  
            user.Email = userRegistration.Email;
            user.Password = userRegistration.Password;

            fundoo.users.Add(user);
            fundoo.SaveChanges();

            if(user !=  null)
            {
                return user;
            }
            else
            {
                return null;
            }
        }


    }
}

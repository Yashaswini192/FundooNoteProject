using BusinessLayer.Interface;
using CommonLayer.Model;
using RepoLayer.Entity;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public  class UserBusiness : IUserBusiness
    {
        private readonly IUserRepo userRepo;

        public UserBusiness(IUserRepo userRepo)
        {
            this.userRepo = userRepo;
        }

        public User Registration(UserRegistration userRegistration)
        {
            try
            {
                return userRepo.Registration(userRegistration);
            }
            catch
            {
                throw;
            }
        }

        public string UserLogin(Login login)
        {
            try
            {
                return userRepo.UserLogin(login);
            }
            catch
            {
                throw;
            }
        }
    }
}

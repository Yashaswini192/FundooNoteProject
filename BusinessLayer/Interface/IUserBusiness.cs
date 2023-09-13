using CommonLayer.Model;
using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IUserBusiness
    {
        public User Registration(UserRegistration userRegistration);

        public string UserLogin(Login login);
    }
}

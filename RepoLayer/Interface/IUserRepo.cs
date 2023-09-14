using CommonLayer.Model;
using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepoLayer.Interface
{
    public interface IUserRepo 
    {
        public User Registration(UserRegistration userRegistration);

        public string UserLogin(Login login);

        public string ForgotPassword(string email);


    }
}

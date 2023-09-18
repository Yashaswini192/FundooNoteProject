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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userRegistration"></param>
        /// <returns></returns>
        public User Registration(UserRegistration userRegistration)
        {
            try
            {
                return userRepo.Registration(userRegistration);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string UserLogin(Login login)
        {
            try
            {
                return userRepo.UserLogin(login);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ForgotPassword(string email)
        {
            try
            {
                return userRepo.ForgotPassword(email);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ResetPassword(ResetPasswordModel resetpassword, string email)
        {
            try
            {
                return userRepo.ResetPassword(resetpassword, email);
            }
            catch(Exception ex) 
            {
                throw ex;
            }
        }

        public List<User> GetALLUsers()
        {
            try
            {
                return userRepo.GetALLUsers();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public User GetUserByUserId(long UserId)
        {
            try
            {
                return userRepo.GetUserByUserId(UserId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}

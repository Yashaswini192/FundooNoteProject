using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepoLayer.Context;
using RepoLayer.Entity;
using RepoLayer.Interface;
using RepoLayer.Migrations;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

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
            try
            {
                User user = new User();

                user.FirstName = userRegistration.FirstName;
                user.LastName = userRegistration.LastName;
                user.Email = userRegistration.Email;
                user.Password = userRegistration.Password;

                fundoo.users.Add(user);
                fundoo.SaveChanges();

                if (user != null)
                {
                    return user;
                }
                else
                {
                    return null;
                }
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

                var user = fundoo.users.FirstOrDefault(c => c.Email == login.Email && c.Password == login.Password);

                if (user != null)
                {
                    string token = GenerateToken(login.Email, user.UserId);
                    return token;

                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public string GenerateToken(string email, long UserId)
        {

            try
            {
                var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTConfig:key"]));
                var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);

                var claims = new[] {
                    new Claim("UserID", UserId.ToString()),
                    new Claim("Email", email),

                };

                var token = new JwtSecurityToken(
                    issuer: configuration["Jwt:Issuer"],
                    audience: configuration["Jwt:Audience"],
                    claims,
                    expires: DateTime.Now.AddHours(1),
                    signingCredentials: credentials
                    );

                return new JwtSecurityTokenHandler().WriteToken(token);
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
                var emailCheck = this.fundoo.users.Where(b => b.Email == email).FirstOrDefault();
                if (emailCheck != null)
                {
                    var token = GenerateToken(emailCheck.Email, emailCheck.UserId);
                    MSMQ msmq = new MSMQ();
                    msmq.SendMessage(token);
                    return token;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool ResetPassword(ResetPasswordModel resetPassword, string email)
        {
            try
            {
                if (resetPassword.newPassword.Equals(resetPassword.confirmPassword))
                {
                    var user = fundoo.users.Where(x => x.Email == email).FirstOrDefault();
                    user.Password = resetPassword.confirmPassword;

                    fundoo.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

       

        public List<User> GetALLUsers()
        {
            try
            {
                var result = fundoo.users.ToList();
                if(result.Count != 0)
                {
                    return result;
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

        public User GetUserByUserId(long UserId)
        {
            try
            {
                var result = fundoo.users.FirstOrDefault(x => x.UserId == (UserId));
                if(result != null)
                {
                    return result;
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


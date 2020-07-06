using Reipush.Api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Internal;
using Reipush.Api.ViewModels;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore.Storage;
using System.Text;


namespace Reipush.Api.Services
{

    public class UserService
    {
        private readonly ReipushContext _reipushcontext;

        public UserService(ReipushContext context)
        {
            _reipushcontext = context;
        }

        public async Task<List<User>> GetAllUsers()
        {
            var iUsers = await  _reipushcontext.User
                                .FromSqlRaw("REIPUSH_GETUSERS")
                                .ToArrayAsync();

            return iUsers.ToList();
        }
        public User GetUserById(viUserId iuser)
        {
            var rUser =  _reipushcontext.User
                        .FromSqlRaw("REIPUSH_GETUSERBYID @UserId",
                         new  SqlParameter("UserId", iuser.UserId))
                        .AsEnumerable()
                        .FirstOrDefault();

            return rUser;
        }

        public User GetUserByEmail(string iemail)
        {
            User rUser = new User();

            try {
                 rUser = _reipushcontext.User
                            .FromSqlRaw("DoesEmailExit @Email",
                             new SqlParameter("Email", iemail))
                            .AsEnumerable()
                            .FirstOrDefault();

            }
            catch (Exception e)
            {
               
            }
            return rUser;
        }

        public voUser GetUserCombineNameById(viUserId iuser)
        {
            var rUser = _reipushcontext.voUser
                        .FromSqlRaw("REIPUSH_GETCOMBINENAMEUSERBYID @UserId",
                         new SqlParameter("UserId", iuser.UserId))
                        .AsEnumerable()
                        .FirstOrDefault();

            return rUser;
        }


        public User CreateUser(User iuser)
        {


            var vUser = _reipushcontext.User.FromSqlRaw("REIPUSH_CREATEUSER  @Email, @FirstName, @LastName, @MobileNumber, @UserType", 
                         new SqlParameter("Email", iuser.Email),
                         new SqlParameter("FirstName", iuser.FirstName),
                         new SqlParameter("LastName", iuser.LastName),
                         new SqlParameter("MobileNumber", iuser.MobileNumber),
                         new SqlParameter("UserType", iuser.UserType)
                         )
                        .AsEnumerable()
                        .FirstOrDefault();

            
            return vUser;
        }


        public User CreateAccount(viEmailPwd iCred)
        {


            User iuser = new User();
            iuser.CreatedOn = DateTime.Now;
            iuser.UpdatedOn = DateTime.Now;
            iuser.Email = iCred.Email;
            iuser.UserType = 0;
            if (iCred.Password != ""){
                iuser.PasswordSalt = CreateSalt(5);             
                iuser.PasswordHash = CreatePasswordHash(iCred.Password, iuser.PasswordSalt);
            }

            User ruserId = _reipushcontext.User.FromSqlRaw("CreateAccount  @Email, @UserType, @PasswordHash, @PasswordSalt",
                         new SqlParameter("Email", iuser.Email),
                         new SqlParameter("UserType", iuser.UserType),
                         new SqlParameter("PasswordHash", iuser.PasswordHash),
                         new SqlParameter("PasswordSalt", iuser.PasswordSalt)
                         ).ToArray().FirstOrDefault();
                        


            return iuser;
        }




        public User ChangeUser(User iuser)
        {
            var iUser = _reipushcontext.User
                        .FromSqlRaw("REIPUSH_GETUSERBYID @UserId",
                         new SqlParameter("UserId", iuser.UserId))
                        .AsEnumerable()
                        .FirstOrDefault();

            return iUser;
        }
        public User DeleteUser(User iuser)
        {
            var iUser = _reipushcontext.User
                        .FromSqlRaw("REIPUSH_GETUSERBYID @UserId",
                         new SqlParameter("UserId", iuser.UserId))
                        .AsEnumerable()
                        .FirstOrDefault();

            return iUser;
        }
        ////////////////////////////////////////////////////////////////////////////////
        //                                   PRIVATE ROUTINES                         //
        /// ////////////////////////////////////////////////////////////////////////////
        private string CreateSalt(int size)
        {
            var provider = new RNGCryptoServiceProvider();
            byte[] data = new byte[size];
            provider.GetBytes(data);
            return Convert.ToBase64String(data);
        }

        private string CreatePasswordHash(string password, string salt)
        {
            //return FormsAuthentication.HashPasswordForStoringInConfigFile(password + salt, "SHA1");
          return  Helper.GetHash(password + salt, "SHA1");
        }

    }

 


}

using CaseStudy.Dtos;
using CaseStudy.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace CaseStudy.Repository
{
    public class LoginRepository : ILoginRepository
    {
        DatabaseContext _context;
        ExceptionRepository _exception;
        public LoginRepository(DatabaseContext context,ExceptionRepository exception)
        {
            _context = context;
            _exception = exception;
        }
        #region
        /// <summary>
        /// Login method
        /// </summary>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public async Task<HttpStatusCode> Login(LoginDto loginUser)
        {
            try
            {
                if (loginUser.role == "Admin")
                {
                    var user = await _context.Admins.SingleOrDefaultAsync(a => a.Email == loginUser.username);
                    if (user == null)
                        return HttpStatusCode.NotFound;

                    else if (!user.Password.Equals(loginUser.password))
                    {
                        return HttpStatusCode.Unauthorized;
                    }
                    return HttpStatusCode.OK;
                }
                else
                {

                    var user = await _context.Users.Include("Role")
                        .SingleOrDefaultAsync(a => a.Email == loginUser.username && a.Role.RoleName == loginUser.role);
                    if (user.Status == "Inactive")
                    {
                        return HttpStatusCode.BadRequest;
                    }
                    else if (user == null)
                        return HttpStatusCode.NotFound;

                    else if (!VerifyPassword(loginUser.password, user.PasswordHash, user.PasswordSalt))
                    {
                        return HttpStatusCode.Unauthorized;
                    }
                    return HttpStatusCode.OK;

                }
            }
            catch (Exception e)
            {
                await _exception.AddException(e, "Login method");
            }
            return HttpStatusCode.BadRequest;
        }
        #endregion
        private bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var passHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return passHash.SequenceEqual(passwordHash);
            }
        }

    }
}

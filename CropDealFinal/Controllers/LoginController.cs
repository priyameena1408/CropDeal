using CaseStudy.Dtos;
using CaseStudy.Models;
using CaseStudy.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace CaseStudy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private DatabaseContext _databaseContext;
        LoginService _loginService;
        IConfiguration _configuration;
        public LoginController(DatabaseContext context, LoginService service, IConfiguration configuration)
        {
            _databaseContext = context;
            _loginService = service;
            _configuration = configuration;
        }
        #region Login
        /// <summary>
        /// Login method
        /// </summary>
        /// <param name="loginData"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Tokens>> Login(LoginDto loginData)
        {
            try
            {
                var res = await _loginService.Login(loginData);

                if (res == HttpStatusCode.OK)
                {

                    if (loginData.role == "Admin")
                    {
                        string userId = _databaseContext.Admins.SingleOrDefault(a => a.Email == loginData.username).AdminId.ToString();
                        var token = GenerateToken(loginData);

                        return new Tokens
                        {
                            Role = loginData.role,
                            UserId = userId,
                            Token = token
                        };
                    }
                    else
                    {
                        string userId = _databaseContext.Users.SingleOrDefault(a => a.Email == loginData.username).UserId.ToString();
                        var token = GenerateToken(loginData);

                        return new Tokens
                        {
                            Role = loginData.role,
                            UserId = userId,
                            Token = token
                        };
                    }
                }

                else if (res == HttpStatusCode.Unauthorized)
                {
                    return Unauthorized("Wrong Password!!");
                }
                else if (res == HttpStatusCode.NotFound)
                {
                    return BadRequest("User not Found. Check username or Register");
                }

                return BadRequest("Some Error Occured");
            }
            catch (Exception e)
            {
                throw;
            }
        }
        #endregion

        #region TokenGeneration
        /// <summary>
        /// Generate TOken
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private string GenerateToken(LoginDto user)
        {
            try { 
            IEnumerable<Claim> claims = new List<Claim>
            {
                new Claim("email",user.username),
                new Claim(ClaimTypes.Role,user.role),
                
            };

            var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("token:key").Value));
            var signCred = new SigningCredentials(symmetricKey,SecurityAlgorithms.HmacSha512);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: signCred);

            
            return new JwtSecurityTokenHandler().WriteToken(token);
            }catch(Exception e)
            {
                throw;
            }
        }
        #endregion

    }
}

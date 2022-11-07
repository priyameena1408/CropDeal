using AutoMapper;
using CaseStudy.Dtos;
using CaseStudy.Dtos.UserDtos;
using CaseStudy.Models;
using CaseStudy.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CaseStudy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private RegisterService _registerService;
        private UserService _userService;
        
        public UserController(RegisterService registerService, UserService userService)
        {
            _registerService = registerService;
            _userService = userService;
            
        }
        #region Register
        /// <summary>
        /// Register User
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<ActionResult<User>> Register(CreateUserDto user)
        {
            try
            {
                var res = await _registerService.RegisterUser(user);
                if (res == null)
                {
                    return BadRequest();
                }
                return Ok(res);
            }
            catch (Exception e)
            {
                throw;
            }
        }
        #endregion

        #region GetUsers
        /// <summary>
        /// List of Users
        /// </summary>
        /// <returns></returns>
        [HttpGet("getUsers")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            try
            {
                var res = await _userService.DisplayUsers();

                if (res == null)
                {
                    return BadRequest();
                }

                return Ok(res);
            }
            catch (Exception e)
            {
                throw;
            }
        }
        #endregion

        [HttpGet("getuser/{id}")]
        public async Task<ActionResult<User>> GetByID(int id)
        {
            var res = await _userService.DisplaySingleuser(id);
            if (res == null)
            {
                return NotFound();
            }
            return Ok(res);
        }

        [HttpPut("updateUser/{id}")]
        public async Task<ActionResult<User>> ChangeDetails(UpdateUserDto givenuser, int id)
        {
            var res = await _userService.ChangeUser(givenuser, id);
            if (res == null)
            {
                return BadRequest();
            }
            return Ok(res);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var res = await _userService.Deleteuser(id);
            if (res == HttpStatusCode.NotFound)
            {
                return NotFound();
            }
            return Ok(res);
        }

        [HttpPut("status/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<User>> ChangeStatus(string stat, int id)
        {
            var res = await _userService.StatusUpdate(stat, id);
            if (res == null)
            {
                return BadRequest();
            }
            return Ok(res);
        }

        [HttpPost("add-rating/{id}")]
        [Authorize]
        public async Task<ActionResult> GiveRating(RatingDto ratinginfo, int id)
        {
            var res = await _userService.AddRating(ratinginfo, id);
            if (res == HttpStatusCode.BadRequest)
            {
                return BadRequest();
            }
            return Ok(res);
        }
 
     
        [HttpGet("getRating/{id}")]
        [Authorize]
        public async Task<Double> GetAvgUserRating(int id)
        {
            var res = await _userService.AvgUserRating(id);
            if(res<0 || res == null)
            {
                return 0.0;
            }
            return res;
        }
    }
}

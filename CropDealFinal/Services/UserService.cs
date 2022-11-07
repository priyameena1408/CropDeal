using CaseStudy.Dtos;
using CaseStudy.Dtos.UserDtos;
using CaseStudy.Models;
using CaseStudy.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CaseStudy.Services
{
	public class UserService
    {
        IUserRepository _repo;
        
	public UserService(IUserRepository repository)
        {
            _repo = repository;
        }

        public async Task<IEnumerable<User>> DisplayUsers()
        {
            return await _repo.GetUsersAsync();
        }
	
	public async Task<ActionResult<User>> DisplaySingleuser(int id)
	{
		return await _repo.GetUserByIDAsync(id);
	}

	public async Task<ActionResult<User>> ChangeUser(UpdateUserDto givenuser, int id)
	{
		return await _repo.UpdateUserAsync(givenuser, id);
    }
	
	public async Task<HttpStatusCode> Deleteuser(int id)
	{
		return await _repo.DeleteUserAsync(id);
	}

	public async Task<ActionResult<User>> StatusUpdate(string stat, int id)
	{
		return await _repo.StatusUpdateAsync(stat,id);
	}

	public async Task<HttpStatusCode> AddRating(RatingDto ratinginfo, int id)
	{
		return await _repo.AddRatingAsync(ratinginfo, id);
	}

	
		public async Task<Double> AvgUserRating(int id)
		{
			return await _repo.AvgUserRating(id);
		}
}
}

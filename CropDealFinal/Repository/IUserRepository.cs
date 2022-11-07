using CaseStudy.Dtos;
using CaseStudy.Dtos.UserDtos;
using CaseStudy.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CaseStudy.Repository
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsersAsync();
	    Task<ActionResult<User>> GetUserByIDAsync(int id);
	    Task<ActionResult<User>> UpdateUserAsync(UpdateUserDto user, int id);
	    Task<HttpStatusCode> DeleteUserAsync(int id);
	    Task<ActionResult<User>> StatusUpdateAsync(string stat, int id);
        Task<HttpStatusCode> AddRatingAsync(RatingDto ratinginfo, int id);
        Task<Double> AvgUserRating(int id);
    }
}

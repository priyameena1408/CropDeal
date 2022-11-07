using CaseStudy.Dtos;
using CaseStudy.Dtos.UserDtos;
using CaseStudy.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using System.Text;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace CaseStudy.Repository
{
	public class UserRepository : IUserRepository
	{
		private enum Role
		{
			Farmer = 1,
			Dealer = 2
		}

		DatabaseContext _context;
		ExceptionRepository _exc;
		public UserRepository(DatabaseContext context, ExceptionRepository exc)
		{
			_context = context;
			_exc = exc;
		}
		#region
/// <summary>
/// Get list of Users
/// </summary>
/// <returns></returns>
		public async Task<IEnumerable<User>> GetUsersAsync()
		{
			try
			{
				return await _context.Users.Include("Account").Include("Address")
					.ToListAsync();
			}
			catch (Exception e)
			{
				await _exc.AddException(e, "GetUsers in UserRepo");           
            }
            return null;
        }
        #endregion

        #region
		/// <summary>
		/// Get Users By Id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
        public async Task<ActionResult<User>> GetUserByIDAsync(int id)
		{
			try
			{
				var user = await _context.Users.Include("Account")
						.Include("Address").SingleOrDefaultAsync(u => u.UserId == id);
				if (user == null)
				{
					return null;
				}
				return user;
			}
			catch (Exception e)
			{
				await _exc.AddException(e, "GetUserById in UserRepo");
                
            }
			return null;

		}
        #endregion

        #region
		/// <summary>
		/// Updating the user
		/// </summary>
		/// <param name="givenUser"></param>
		/// <param name="id"></param>
		/// <returns></returns>
        public async Task<ActionResult<User>> UpdateUserAsync(UpdateUserDto givenUser, int id)
		{
			try
			{
				var user = await _context.Users
						.Include("Address")
						.Include("Account")
						.SingleOrDefaultAsync(u => u.UserId == id);
				if (user == null)
				{
					return null;
				}
				user.Email = givenUser.Email;
				user.Phone = givenUser.Phone;

				//var address = new Address();
				user.Address.Line = givenUser.Line;
				user.Address.City = givenUser.City;
				user.Address.State = givenUser.State;
				//user.Address = address;

				//Account acc = new Account();
				user.Account.AccountNumber = givenUser.AccountNumber;
				user.Account.IFSCCode = givenUser.IFSC;
				user.Account.BankName = givenUser.BankName;
				//	user.Account = acc;

				_context.Update(user);
				await _context.SaveChangesAsync();
				return user;
			}
			catch (Exception e)
			{
				await _exc.AddException(e, "Update User in UserRepo");
			}
			return null;
		}
        #endregion
        #region Delete User
        /// <summary>
        /// Delete specific User
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<HttpStatusCode> DeleteUserAsync(int id)
		{
			try
			{
				_context.Remove(_context.Users.Single(u => u.UserId == id));
				await _context.SaveChangesAsync();
				return HttpStatusCode.OK;
			}
			catch (Exception e)
			{
                await _exc.AddException(e, "DeleteUser in UserRepo");
            }
			return HttpStatusCode.NotFound;
		}
        #endregion
        #region UpdateStatus
        /// <summary>
        /// Update the status of User
        /// </summary>
        /// <param name="stat"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult<User>> StatusUpdateAsync(string stat, int id)
		{
			try
			{
				User user = await _context.Users.SingleOrDefaultAsync(u => u.UserId == id);
				user.Status = stat;
				_context.Users.Update(user);
				await _context.SaveChangesAsync();
				return user;
			}
			catch (Exception e)
			{
                await _exc.AddException(e, "UpdateStatus in UserRepo");
            }
			return null;
		}

        #endregion
        #region AddRating
		/// <summary>
		/// Give Rating to Farmer only
		/// </summary>
		/// <param name="ratinginfo"></param>
		/// <param name="id"></param>
		/// <returns></returns>
        public async Task<HttpStatusCode> AddRatingAsync(RatingDto ratinginfo, int id)
		{
			try {
				Rating rating = new Rating();
				rating.TotalRating = ratinginfo.TotalRating;
				rating.Review = ratinginfo.Review;
				rating.UserId = id;
				_context.Ratings.Add(rating);
				await _context.SaveChangesAsync();
				return HttpStatusCode.OK;
			}
			catch (Exception e)
			{
                await _exc.AddException(e, "AddRating in UserRepo");
            }
			return HttpStatusCode.BadRequest;
		}
        #endregion
        
		
		#region AvgRating
		/// <summary>
		/// 
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
        public async Task<Double> AvgUserRating(int id)
		{
			try
			{
				var avg = await _context.Ratings
						.Where(p => p.UserId == id)
						.AverageAsync(p => p.TotalRating);
				return avg;
			}
			catch (Exception e)
			{
                await _exc.AddException(e, "Average Rating in UserRepo");
            }
			return 0.0;
		}
        #endregion
    }
}

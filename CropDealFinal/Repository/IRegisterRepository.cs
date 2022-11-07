using CaseStudy.Dtos.UserDtos;
using CaseStudy.Models;
using Microsoft.AspNetCore.Mvc;

namespace CaseStudy.Repository
{
    public interface IRegisterRepository
    {
        Task<ActionResult<User>> CreateUserAsync(CreateUserDto user);
    }
}

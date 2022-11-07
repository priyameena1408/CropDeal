using CaseStudy.Dtos.UserDtos;
using CaseStudy.Models;
using CaseStudy.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CaseStudy.Services
{
    public class RegisterService
    {
        IRegisterRepository _repo;
        public RegisterService(IRegisterRepository repository)
        {
            _repo = repository;
        }

        public async Task<ActionResult<User>> RegisterUser(CreateUserDto newUser)
        {
            return await _repo.CreateUserAsync(newUser);
        }
    }
}

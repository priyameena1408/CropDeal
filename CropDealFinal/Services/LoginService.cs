using CaseStudy.Dtos;
using CaseStudy.Repository;
using System.Net;

namespace CaseStudy.Services
{
    public class LoginService
    {
        ILoginRepository _repo;
        public LoginService(ILoginRepository repo)
        {
            _repo = repo;
        }

        public async Task<HttpStatusCode> Login(LoginDto loginDto)
        {
            return await _repo.Login(loginDto);
        }
    }
}

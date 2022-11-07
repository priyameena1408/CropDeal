using CaseStudy.Dtos;
using System.Net;

namespace CaseStudy.Repository
{
    public interface ILoginRepository
    {
        Task<HttpStatusCode> Login(LoginDto loginUser);
    }
}

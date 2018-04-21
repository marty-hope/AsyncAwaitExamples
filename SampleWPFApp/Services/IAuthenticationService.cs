using System.Threading.Tasks;

namespace SampleWPFApp.Services
{
    public interface IAuthenticationService
    {
        Task<bool> AuthenticateUser(string userName, string password);
    }
}
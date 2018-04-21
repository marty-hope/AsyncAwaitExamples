using SampleWPFApp.Services;
using System.Threading.Tasks;

namespace SampleWPFApp
{
    internal class AuthenticationService : IAuthenticationService
    {
        public async Task<bool> AuthenticateUser(string userName, string password)
        {
            await Task.Delay(3000);
            return true;
        }
    }
}
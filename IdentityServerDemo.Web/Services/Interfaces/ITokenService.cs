
using IdentityModel.Client;
using System.Threading.Tasks;

namespace IdentityServerDemo.Web.Services.Interfaces
{
    public interface ITokenService
    {
        Task<TokenResponse> GetToken (string scope);
    }
}

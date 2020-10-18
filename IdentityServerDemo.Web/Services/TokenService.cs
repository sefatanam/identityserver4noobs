using IdentityModel.Client;
using IdentityServerDemo.Web.Services.Interfaces;
using System.Net.Http;
using System.Threading.Tasks;

namespace IdentityServerDemo.Web.Services
{
    public class TokenService : ITokenService
    {
        /// <summary>
        ///  Here is the DiscoveryDocumentReponse class that comes with the package that we installed earlier.
        /// </summary>
        private DiscoveryDocumentResponse _discDocument { get; set; }

        /// <summary>
        /// in the constructor we use the HTTPClient to get the Document data from the IdentityServer OpenID Configuration endpoint.
        /// Note that we are hardcoding the URLs here.
        /// Ideally, we will have to define them in appsettings.json and use IOptions pattern to retrieve them at runtime.
        /// </summary>
        public TokenService ()
        {
           
            using (var client = new HttpClient())
            {
                _discDocument = client.GetDiscoveryDocumentAsync("https://localhost:44368/.well-known/openid-configuration").Result;
            }
        }
        public async Task<TokenResponse> GetToken (string scope)
        {
            using (var client = new HttpClient())
            {
                var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
                {
                    Address = _discDocument.TokenEndpoint,
                    ClientId = "cwm.client",
                    Scope = scope,
                    ClientSecret = "secret"
                });
                if (tokenResponse.IsError)
                {
                    throw new System.Exception("Token Error");
                }
                return tokenResponse;
            }
        }

    }
}

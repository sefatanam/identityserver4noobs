using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;

namespace IdentityServerDemo
{
    public class IdentityConfiguration
    {

        /// <summary>
        /// Inspired From : codewithmukesh.com
        /// Original Link : https://www.codewithmukesh.com/blog/identityserver4-in-aspnet-core/
        /// This snippet returns a TestUser with some specific JWT Claims.
        /// </summary>
        public static List<TestUser> TestUsers => new List<TestUser> { new TestUser {
            SubjectId = "1703", Username = "sefat", Password = "sefat",
            Claims = {
                new Claim(JwtClaimTypes.Name, "Sefat Anam"),
                new Claim(JwtClaimTypes.GivenName, "Sefat"),
                new Claim(JwtClaimTypes.FamilyName, "Anam"),
                new Claim(JwtClaimTypes.WebSite, "https://sefatanam.github.io/me/"),

            }
        }};

        /// <summary>
        /// Identity Resources
        /// Identity Resources are data like userId, email, a phone number that is something unique to a particular identity/user
        /// </summary>
        public static IEnumerable<IdentityResource> IdentityResources => new IdentityResource[] {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        };


        /// <summary>
        /// API Scopes
        /// As mentioned earlier, our main intention is to secure an API (which we have not built yet.). So, this API can have scopes.Scopes in the context of,
        /// what the authorized user can do.
        /// For example, we can have 2 scopes for now – Read, Write.Let’s name our API as myAPI.
        /// </summary>
        public static IEnumerable<ApiScope> ApiScopes => new ApiScope[] {
             new ApiScope("myApi.read"),
             new ApiScope("myApi.write"),
        };


        /// <summary>
        /// API Resources
        /// Now, let’s define the API itself.We will give it a name myApi and mention the supported scopes as well, along with the secret.
        /// Ensure to hash this secret code. This hashed code will be saved internally within IdentityServer.
        /// </summary>
        public static IEnumerable<ApiResource> ApiResources => new ApiResource[] {
            new ApiResource("myApi"){
            Scopes = new List<string>{ "myApi.read","myApi.write" },
            ApiSecrets = new List<Secret>{ new Secret("supersecret".Sha256()) }
            }
        };


        /// <summary>
        /// Clients
        /// Finally, we have to define who will be granted access to our protected resource which in our case is myApi.
        /// Give an appropriate client name and Id.Here we are setting the GrantType as ClientCredentials.
        /// </summary>
        public static IEnumerable<Client> Clients => new Client[] {
            new Client{
            ClientId = "cwm.client",
            ClientName = "Client Credentials Client",
            AllowedGrantTypes = GrantTypes.ClientCredentials,
            ClientSecrets = { new Secret("secret".Sha256()) },
            AllowedScopes = { "myApi.read" }
            },
        };



        /* Last Step To Follow
         * 
         * Registering IdentityServer4 in ASP.NET Core
         * Let’s register IdentityServer4 in ASP.NET Core DI Container. Open up Startup.cs and add the following to the ConfigureServices method.
         * Here will be using all the Static Resources, Clients, and Users we had defined in our IdentityConfiguration class.
         */
    }


}

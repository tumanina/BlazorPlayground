using Microsoft.AspNetCore.Components.WebAssembly.Authentication.Internal;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using System.Security.Claims;
using System.Text.Json.Serialization;

namespace BlazorPlayground.Web.Middleware
{
    public class CustomAccountFactory : AccountClaimsPrincipalFactory<CustomUserAccount>
    {
        public CustomAccountFactory(IAccessTokenProviderAccessor accessor)
            : base(accessor)
        {

        }

        public async override ValueTask<ClaimsPrincipal> CreateUserAsync(CustomUserAccount account, RemoteAuthenticationUserOptions options)
        {
            var initialUser = await base.CreateUserAsync(account, options);

            //temporary solution, account.Roles should be used 
            var admins = new List<string>
            {
                "John Doe",
            };
            if (initialUser.Identity.IsAuthenticated)
            {
                var userIdentity = (ClaimsIdentity)initialUser.Identity;
                var name = userIdentity.Claims.FirstOrDefault(c => c.Type == "name")?.Value;
                if (admins.Contains(name))
                {
                    userIdentity.AddClaim(new Claim(ClaimTypes.Role, "Write"));
                }
            }

            return initialUser;
        }
    }

    public class CustomUserAccount : RemoteUserAccount
    {
        [JsonPropertyName("groups")]
        public string[] Groups { get; set; }

        [JsonPropertyName("roles")]
        public string[] Roles { get; set; }
    }
}

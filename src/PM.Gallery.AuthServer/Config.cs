using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace PM.Gallery.AuthServer;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new List<ApiScope>
        {
            new ApiScope(name: "gallery_api", displayName: "Access to Gallery API"),
        };

    public static IEnumerable<Client> Clients =>
        new List<Client>
        {
            new Client
            {
                ClientId = "vue-client",

                AllowedGrantTypes = GrantTypes.Code,

                // where to redirect to after login
                RedirectUris = { "https://localhost:5002/signin-oidc", "https://localhost:9001/oidc-callback", "https://127.0.0.1:9001/oidc-callback" },

                // where to redirect to after logout
                PostLogoutRedirectUris = { "https://localhost:5002/signout-callback-oidc" },

                // AllowedCorsOrigins = { "https://localhost:9001","http://localhost:9001" },
                
                // AllowOfflineAccess = true,
                RequireClientSecret = false,
                RequirePkce = true,
                // RequireConsent = true,

                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "gallery_api",
                },
            },
            new Client
            {
                ClientId = "swagger_api",
                
                // ClientSecrets =  new List<Secret>()
                // {
                //     new Secret("secret".Sha256())
                // },
                
                AllowedGrantTypes = GrantTypes.Implicit,
                
                AllowAccessTokensViaBrowser=true,
                
                RedirectUris = { "https://localhost:7032/swagger/oauth2-redirect.html"},
                
                AllowedScopes =
                {
                    "gallery_api"
                }
            }
        };
}
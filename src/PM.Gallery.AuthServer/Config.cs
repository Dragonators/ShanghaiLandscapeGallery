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
            new ApiScope(name: "usermanager_api", displayName: "Access to UserManager API")
        };

    public static IEnumerable<Client> Clients =>
        new List<Client>
        {
            new Client
            {
                ClientId = "vue-client",

                AllowedGrantTypes = GrantTypes.Code,

                // where to redirect to after login
                RedirectUris = { "https://localhost:5002/signin-oidc", "https://localhost:9001/oidc-callback" },

                // where to redirect to after logout
                PostLogoutRedirectUris = { "https://localhost:5002/signout-callback-oidc" },

                AllowedCorsOrigins = { "https://localhost:9001" },

                // AllowOfflineAccess = true,
                RequireClientSecret = false,
                RequirePkce = true,
                // RequireConsent = true,

                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "gallery_api",
                    "usermanager_api",
                },
            }
        };
}
using Duende.IdentityServer.AspNetIdentity;
using Duende.IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using IdentityModel;
using PM.Gallery.AuthServer.Models;

namespace PM.Gallery.AuthServer
{
    public class GalleryProfileService : ProfileService<ApplicationUser>
    {
        protected UserManager<ApplicationUser> _userManager;

        public GalleryProfileService(UserManager<ApplicationUser> userManager,
            IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory) : base(userManager, claimsFactory)
        {
            _userManager = userManager;
        }

        protected override async Task GetProfileDataAsync(ProfileDataRequestContext context, ApplicationUser user)
        {
            var claims = new List<Claim>()
            {
                new("username", user.UserName),
                new(JwtClaimTypes.Email, user.Email ?? string.Empty),
            };

            (await _userManager.GetRolesAsync(user))?.ToList()
                .ForEach(r => claims.Add(new Claim(JwtClaimTypes.Role, r)));

            context.AddRequestedClaims(claims);
        }
    }
}
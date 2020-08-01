﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Agronomous
{
    public class user : IdentityUser
    {
        public Guid plantGuid { get; set; }
    }
    public class PlantClaimsPrincipalFactory : UserClaimsPrincipalFactory<user, IdentityRole>
    {
        public PlantClaimsPrincipalFactory(
            UserManager<user> userManager,
            RoleManager<IdentityRole> roleManager,
            IOptions<IdentityOptions> optionsAccessor
            ) : base(userManager,roleManager,optionsAccessor)
        { }
        public async override Task<ClaimsPrincipal> CreateAsync(user user)
        {
            var principal = await base.CreateAsync(user);
            if (!(user.plantGuid.Equals(null)))
            {
                ((ClaimsIdentity)principal.Identity).AddClaim(new Claim(ClaimTypes.Hash, user.plantGuid.ToString()));
            }
            return principal;
        }
    }
    public static class IdentityExtensions
    {
        public static string GardenGuid(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst(ClaimTypes.Hash);
            return (claim != null) ? claim.Value : string.Empty;
        }
        public static Guid editGardenGuid(this IPrincipal currentPrincipal)
        {
            Guid gardenGuid = Guid.NewGuid();
            var identity = currentPrincipal.Identity as ClaimsIdentity;
            if (identity == null)
            {
                return new Guid();
            }
            var existingClaim = identity.FindFirst(ClaimTypes.Hash);
            if (existingClaim != null)
            {
                identity.RemoveClaim(existingClaim);
            }
            identity.AddClaim(new Claim(ClaimTypes.Hash, gardenGuid.ToString()));
            return gardenGuid;
        }
    }
}

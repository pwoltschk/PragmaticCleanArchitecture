﻿using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication.Internal;
using Shared.Identity;
using System.Security.Claims;
using System.Text.Json;

namespace UI.Identity
{
    public class PermissionAccountClaimsPrincipalFactory : AccountClaimsPrincipalFactory<RemoteUserAccount>
    {
        public PermissionAccountClaimsPrincipalFactory(IAccessTokenProviderAccessor accessor) : base(accessor) { }

        public async override ValueTask<ClaimsPrincipal> CreateUserAsync(RemoteUserAccount account, RemoteAuthenticationUserOptions options)
        {
            if (account is null)
                return new ClaimsPrincipal();

            var user = await base.CreateUserAsync(account, options);
            var identity = user.Identity as ClaimsIdentity;

            if (account.AdditionalProperties.ContainsKey(nameof(Permission)))
            {
                var permissionsJson = account.AdditionalProperties[nameof(Permission)] as JsonElement?;

                permissionsJson?
                    .EnumerateArray()
                    .ToList()
                    .ForEach(permission => identity?
                    .AddClaim(new Claim(nameof(Permission), permission.GetString() ?? string.Empty)));
            }

            return user;
        }

    }
}

// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;
using System.Security.Claims;

namespace IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> Ids =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResource("rights", "rights", new string[] {"rights"}),
                new IdentityResource("role", "role", new string[] {"role"}),
            };

        public static IEnumerable<ApiResource> Apis =>
            new ApiResource[]
            {
                new ApiResource("api1", "api1")
                {
                    Scopes =
                    {
                        new Scope(){Name = "api1.full_access"},
                        new Scope(){Name = "api1.read_only"}
                    }
                }, 
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client
                {
                    ClientId = "mvc",
                    ClientSecrets = {new Secret("secret".Sha256())},

                    AllowedGrantTypes = GrantTypes.Code,
                    RequireConsent = true,

                    RedirectUris = {"http://localhost:5011/signin-oidc"},

                    AlwaysIncludeUserClaimsInIdToken = true,

                    AllowOfflineAccess = true,

                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "rights",
                        "role"
                    },
                },
                new Client()
                {
                    ClientId = "app",
                    ClientSecrets = {new Secret("secret".Sha256())},

                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    
                    AllowedScopes = new List<string>
                    {
                        "api1.full_access",
                        "api1.read_only"
                    },
                }
            };
    }
}
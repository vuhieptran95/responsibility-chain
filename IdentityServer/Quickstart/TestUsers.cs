// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System.Collections.Generic;
using System.Security.Claims;
using IdentityModel;
using IdentityServer4.Test;

namespace IdentityServer.Quickstart
{
    public class TestUsers
    {
        public static List<TestUser> Users = new List<TestUser>
        {
            new TestUser{SubjectId = "pmo1", Username = "pmo1", Password = "pmo1", 
                Claims = 
                {
                    new Claim(JwtClaimTypes.Name, "pmo1"),
                    new Claim(JwtClaimTypes.Role, "PMO"),
                    new Claim(JwtClaimTypes.Email, "pmo1@phr.com"),
                    new Claim("rights","phr.item1:read phr.item2:read phr.item1:update phr.item2:update")
                }
            }
        };
    }
}
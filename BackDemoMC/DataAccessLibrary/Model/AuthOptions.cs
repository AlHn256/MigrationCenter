// <copyright file="AuthOptions.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ApiDemoMC.Model
{
    using System.Text;
    using Microsoft.IdentityModel.Tokens;

    public class AuthOptions
    {
        public const string ISSUER = "MyAuthServer"; // издатель токена

        public const string AUDIENCE = "http://localhost:51884/"; // потребитель токена

        public const int LIFETIME = 60; // время жизни токена

        private const string KEY = "myretsupecret_secy!123ersek";

        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
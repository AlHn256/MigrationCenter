// <copyright file="UserMigrant.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace DataAccessLibrary
{
    public class UserMigrant
    {
        public int UserId { get; set; }

        public UrAgent UrAgent { get; set; }

        public int MigrantId { get; set; }

        public Migrant Migrant { get; set; }
    }
}

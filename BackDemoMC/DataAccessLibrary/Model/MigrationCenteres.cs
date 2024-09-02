// <copyright file="MigrationCenteres.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace DataAccessLibrary
{
    using System.ComponentModel.DataAnnotations;

    public class MigrationCenteres
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Adress { get; set; }

        public string Phone { get; set; }

        public string Mail { get; set; }

        public string URL { get; set; }

        public bool IsDelete { get; set; }
    }
}

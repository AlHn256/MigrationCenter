// <copyright file="SimpleOrderDTO.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ApiDemoMC.DTO
{
    using DataAccessLibrary.Enum;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class SimpleOrderDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public EnumOrderStatus Status { get; set; }

        public string Date { get; set; }

        public int Cost { get; set; }
    }
}

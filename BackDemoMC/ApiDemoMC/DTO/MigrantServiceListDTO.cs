// <copyright file="MigrantServiceListDTO.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ApiDemoMC.DTO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class MigrantServiceListDTO
    {
        public int MigrantId { get; set; }

        public string MigrantName { get; set; }

        public string PassNumber { get; set; }

        public string PassSerri { get; set; }

        public List<int> ServicesId { get; set; }

        public List<ServiceDTO> ServicesList { get; set; }
    }
}

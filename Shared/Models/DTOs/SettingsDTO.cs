﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models.DTOs
{
    public class SettingsGenerateMenuDTO
    {
        public bool GenerateProducts { get; set; }
    }

    public class SettingsMenuTitlesDTO
    {
        [StringLength(50, MinimumLength = 1)]
        public string FirstTitle { get; set; }

        [StringLength(50, MinimumLength = 1)]
        public string SecondTitle { get; set; }
    }

    public class SettingsPlaceInformationDTO
    {
        [StringLength(250, MinimumLength = 0)]
        public string Address { get; set; }

        [StringLength(250, MinimumLength = 0)]
        public string OpenHours { get; set; }

        [StringLength(250, MinimumLength = 0)]
        public string Phones { get; set; }

        [StringLength(250, MinimumLength = 0)]
        public string Emails { get; set; }
    }

    public class SettingsOnlineOptionsDTO
    {
        public bool IsOnlineActive { get; set; }
        public bool HasHomeDelivery { get; set; }
        public decimal ShippingCost { get; set; }
    }

}

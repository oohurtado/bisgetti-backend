using System;
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
        [StringLength(250, MinimumLength = 1)]
        public string Address { get; set; }

        [StringLength(250, MinimumLength = 1)]
        public string OpenHours { get; set; }

        [StringLength(250, MinimumLength = 1)]
        public string Phones { get; set; }

        [StringLength(250, MinimumLength = 1)]
        public string Emails { get; set; }
    }

}

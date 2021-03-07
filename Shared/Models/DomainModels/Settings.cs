using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Models.DomainModels
{
    public class Settings
    {
        // relationships
        public int Id { get; set; }

        // fields
        public bool IsOnlineActive { get; set; }
        public bool HasHomeDelivery { get; set; }
        public decimal ShippingCost { get; set; }

        #region Menu
        public string MenuProductsJson { get; set; }
        #endregion

        #region More jsons
        public string MenuMessagesJson { get; set; }
        public string PlaceInformationJson { get; set; } 
        #endregion
    }
}

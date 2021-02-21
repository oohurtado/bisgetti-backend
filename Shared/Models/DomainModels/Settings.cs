﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Models.DomainModels
{
    public class Settings
    {
        // relationships
        public int Id { get; set; }

        // fields
        #region Menu information
        public string MenuMsgTitle { get; set; }
        public string MenuMsgDescription { get; set; }
        #endregion

        #region Open days, hours and more information
        public bool IsOnlineActive { get; set; }
        public string PlaceInformationJson { get; set; }
        #endregion

        #region home delivery
        public bool HasHomeDelivery { get; set; }
        public decimal ShippingCost { get; set; }
        #endregion

        #region current menu
        public string MenuVersion { get; set; }
        public string MenuProductsJson { get; set; }
        #endregion
    }
}

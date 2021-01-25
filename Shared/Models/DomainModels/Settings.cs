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
        #region Menu information
        public string MsgTitle { get; set; }
        public string MsgDescription { get; set; }
        public string MsgExtra { get; set; }
        #endregion
        #region Open days, hours and more information
        public bool OnlineActive { get; set; }
        public string PlaceInformationJson { get; set; }
        #endregion
        #region home delivery
        public bool HomeDelivery { get; set; }
        public decimal ShippingCost { get; set; }
        #endregion
        #region current menu
        public string MenuVersion { get; set; }
        public string MenuJson { get; set; } // object Menu
        #endregion    

        // TODO: necesito campo para relacionar a persona owner
    }
}

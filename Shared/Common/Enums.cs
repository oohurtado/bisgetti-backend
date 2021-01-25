using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Common
{
    public enum PersonType
    {
        [PersonTypeAttributes(Name = "Propietario", IsEmployee = true, IsClient = false, Role = "Owner")]
        Owner,

        [PersonTypeAttributes(Name = "Cliente", IsEmployee = false, IsClient = true, Role = "Client")]
        Client,

        [PersonTypeAttributes(Name = "Anónimo", IsEmployee = false, IsClient = true, Role = "Anonymous")]
        Anonymous,
    }
}

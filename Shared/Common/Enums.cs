using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Common
{
    public enum PersonType
    {
        [PersonTypeAttributes(Name = "Propietario", IsEmployee = true, IsClient = false, Role = "OWNER")]
        Owner,

        [PersonTypeAttributes(Name = "Cliente", IsEmployee = false, IsClient = true, Role = "CLIENT")]
        Client,

        [PersonTypeAttributes(Name = "Anónimo", IsEmployee = false, IsClient = true, Role = "ANONYMOUS")]
        Anonymous,
    }
}

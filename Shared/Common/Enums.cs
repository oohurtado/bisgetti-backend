using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

    public enum ResponseMessageType
    {
        [Display(Name = "Error desconocido, contacte a su administrador")]
        UnknownError,

        [Display(Name = "Error, no pudimos encontrar lo que estás busando")]
        NotFound,

        [Display(Name = "Error, las credenciales son incorrectas, intenta nuevamente")]
        WrongCredentials,

        [Display(Name = "Error, el servidor no pudo entender la solicitud debido a una sintaxis incorrecta")]
        BadRequest,
    }
}

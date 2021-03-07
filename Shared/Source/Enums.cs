using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Shared.Source
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

    public enum ProductType
    {
        [Display(Name = "Comida")]
        Food,

        [Display(Name = "Bebida")]
        Drink,
    }

    public enum ProductAvailability
    {
        [Display(Name = "Localmente y en línea")]
        Complete,

        [Display(Name = "Sólo localmente")]
        Local,

        [Display(Name = "Sólo en línea")]
        Online,
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

        [Display(Name = "Error, este correo electrónico ya existe")]
        AlreadyExist,
    }
}

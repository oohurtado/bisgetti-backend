using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Shared.Common
{
    public static class EnumExtension
    {
        #region PersonTypeAttributes
        public static string GetPersonTypeName(this Enum enumValue)
        {
            return enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .First()
                            .GetCustomAttribute<PersonTypeAttributes>()
                            .Name;
        }

        public static string GetPersonTypeRole(this Enum enumValue)
        {
            return enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .First()
                            .GetCustomAttribute<PersonTypeAttributes>()
                            .Role;
        }

        public static bool IsPersonTypeEmployee(this Enum enumValue)
        {
            return enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .First()
                            .GetCustomAttribute<PersonTypeAttributes>()
                            .IsEmployee;
        }

        public static bool IsPersonTypeClient(this Enum enumValue)
        {
            return enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .First()
                            .GetCustomAttribute<PersonTypeAttributes>()
                            .IsClient;
        } 
        #endregion

        public static string GetName(this Enum enumValue)
        {
            return enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .First()
                            .GetCustomAttribute<DisplayAttribute>()
                            .Name;
        }
    }

    public static class ClaimTypes
    {
        public const string PersonId = "http://schemas.xmlsoap.org/ws/2021/identity/claims/personId";
        public const string UserId = "http://schemas.xmlsoap.org/ws/2021/identity/claims/userId";
    }
}

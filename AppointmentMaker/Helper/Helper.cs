using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace AppointmentMaker.Helper
{
    public static class Helper
    {
        public static string Admin = "Admin";
        public static string User = "User";


        public static List<SelectListItem> GetRolesForDropDown()
        {
            return new List<SelectListItem>
            {
                new SelectListItem{Value =Helper.Admin, Text=Helper.Admin},
                new SelectListItem{Value =Helper.User, Text=Helper.User},
            };
        }
    }
}

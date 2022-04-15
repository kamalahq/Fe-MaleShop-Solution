using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Fe_MaleShop.WebUI.AppCode.Extensions
{
    static public partial class Extension
    {
        static public bool IsEmail(this string value)
        {
            bool success = Regex.IsMatch(value, @"^(?<email>[\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
                return success;
        }
    }
}

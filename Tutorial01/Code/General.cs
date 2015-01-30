using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tutorial01.Code
{
    public static class ExtensionMethods
    {
        public static string TarifficCase(this string str)
        {
            string tarrifCase = string.Empty;
            
            if(str.Length>2)
            {
                char[] arr = str.ToCharArray();
                arr[0] = char.ToUpper(arr[0]);
                arr[arr.Length - 1] = char.ToUpper(arr[arr.Length - 1]);
                return new string(arr);
            }
            return str;
        }
    }
}
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FromsSystem
{
    public static class JsonHelper
    {
        public static string ToJsonCon<T>(this T data)
        {
            return JsonConvert.SerializeObject(data);
        }
    }
}
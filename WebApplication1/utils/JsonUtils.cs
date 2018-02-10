using System;
using System.Text.RegularExpressions;

namespace WebApplication1.utils
{
    public class JsonUtils
    {
        
        public static string ReplaceJsonDateToDateString(string json)
        {
            return Regex.Replace(json, @"\\/Date\((\d+)\)\\/", match =>
            {
                DateTime dt = new DateTime(1970, 1, 1);
                dt = dt.AddMilliseconds(long.Parse(match.Groups[1].Value));
                dt = dt.ToLocalTime();
                return dt.ToString("yyyy-MM-dd HH:mm:ss");
            });
        }
    }
}
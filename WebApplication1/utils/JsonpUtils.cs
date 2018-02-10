using System;
using System.Net.Http;
using System.Text;
using System.Web.Script.Serialization;

namespace WebApplication1.utils
{
    public class JsonpUtils
    {
        public static HttpResponseMessage doJsonP(CommonResult commonResult, String callback)
        {
            string str = "";
            if (callback == null || callback == "")
            {
                str = new JavaScriptSerializer().Serialize(commonResult);
            } else
            {
                str = callback + "(" + new JavaScriptSerializer().Serialize(commonResult) + ")";
            }

            string res = JsonUtils.ReplaceJsonDateToDateString(str);

            HttpResponseMessage result = new HttpResponseMessage { Content = new StringContent(res, Encoding.GetEncoding("UTF-8"), "application/json") };
            
            return result;
        }
    }
}
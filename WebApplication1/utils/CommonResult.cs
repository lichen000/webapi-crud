using System;

namespace WebApplication1.utils
{
    public class CommonResult
    {
        public int code = CommonStatusCode.OK_CODE;
        public string message = CommonMesage.OK_MESSAGE;
        public Object data = null;
        public Int64 timestamp;

        public CommonResult()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            timestamp = Convert.ToInt64(ts.TotalMilliseconds);            
        }
    }
}
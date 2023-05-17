

using System.Globalization;

namespace Restaurant.Core.Application.Exceptions
{
    public class ApiExeption : Exception
    {
        public int ErrorCode { get; set; }

        public ApiExeption() : base() { }
        public ApiExeption(string message) : base(message) { } 
        public ApiExeption(string message, int errorCode) : base(message) {
           ErrorCode = errorCode;
        }
        public ApiExeption(string message, params object[] args) : base(String.Format(CultureInfo.CurrentCulture,message,args)) { }


    }
}

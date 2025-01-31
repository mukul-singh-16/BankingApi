using System.Security.Cryptography.X509Certificates;

namespace BankingApp.Core.Exceptions
{
    public class ValidationException 
    {
        public int statusCode { get; set; } 
        public string message { get; set; }

        public string body {  get; set; }   




        public ValidationException(int statusCode, string message)
        {
            this.statusCode = statusCode;
            this.message = message;
        }


        public ValidationException(int statusCode, string message,string body)
        {
            this.statusCode = statusCode;
            this.message = message;
        }



    }
}

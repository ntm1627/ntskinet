using System;

namespace API.Errors
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode, string message=null)//the error may  not have a message so default is set to null
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(StatusCode); //?? is null collasing operator
        }

        public int StatusCode { get; set; }
        public string Message { get; set; }

        
        private string GetDefaultMessageForStatusCode(int statusCode)
        {
            return statusCode switch  //it is a new swich in C# v8 without case
            {
                400 =>"A bad request, you have made",
                401 =>"Authorized, you are not",
                404 =>"Resource found, it was not",
                500 =>"Errors are the path to the dark side. Errors lead to anger. Anger leads hate. Hate leads to career change",
                  _  =>null  //default case underscore
            };
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LCP.ApiResponses
{
    public class ApiResponse
    {
        /// <summary>
        /// Base on the status code, the relevant message will be added if message is null
        /// </summary>
        /// <param name="statusCode"></param>
        /// <param name="message"></param>
        public ApiResponse(int statusCode, string? message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetStatusCodeMessage(statusCode);
        }

        public int StatusCode { get; set; }
        public string? Message { get; set; }

        private string? GetStatusCodeMessage(int statusCode) => statusCode switch
        {
            400 => "You have made a bad request",
            401 => "You are not Authorized",
            404 => "Resource was not found",
            500 => "An error occured at server level.",
            _ => null
        };
    }
}

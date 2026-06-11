using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Common
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }


        public ApiResponse(bool success, string message, T data)
        {
            Success = success;
            Message = message;
            Data = data;
        }

        //en caso de fallo
        public static ApiResponse <object> Fail (string message)
        {
            return new ApiResponse<object>(false, message, null);
        }

        //OK
        public static ApiResponse<T> Ok(T data, string message)
        {
            return new ApiResponse<T>(true, message, data);
        }

    }
}

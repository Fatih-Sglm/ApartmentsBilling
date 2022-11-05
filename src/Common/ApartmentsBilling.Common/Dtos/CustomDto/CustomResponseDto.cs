using System.Collections.Generic;

namespace ApartmentsBilling.Common.Dtos.CustomDto
{
    public class CustomResponseDto<T>
    {
        public T Data { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public List<string> Errors { get; set; }
        public static CustomResponseDto<T> SuccesWithData(T data, string message = null)
        {
            return new CustomResponseDto<T> { Data = data, Message = message };
        }

        public static CustomResponseDto<T> SuccesWithOutData(string message = null)
        {
            return new CustomResponseDto<T> { Message = message };
        }

        public static CustomResponseDto<T> Fail(List<string> Errors)
        {
            return new CustomResponseDto<T> { Errors = Errors };
        }
        public static CustomResponseDto<T> Fail(int StatusCode, List<string> Errors)
        {
            return new CustomResponseDto<T> { StatusCode = StatusCode, Errors = Errors };
        }

        public static CustomResponseDto<T> Fail(string Error)
        {
            return new CustomResponseDto<T> { Errors = new List<string> { Error } };
        }

        public static CustomResponseDto<T> Fail(int statuscode, string Error)
        {
            return new CustomResponseDto<T> { StatusCode = statuscode, Errors = new List<string> { Error } };
        }

    }
}

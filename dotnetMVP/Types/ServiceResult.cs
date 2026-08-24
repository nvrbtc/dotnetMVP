using dotnetMVP.Models.DTO.User;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace dotnetMVP.Types
{
    public class ServiceResult<T>
    {
        public T? Data { get; set; }
        public bool Success { get; set; }   // default val 
        public string? Message { get; set; }
        public OperationResult OpResult { get; set; } // def val 
        private ServiceResult(bool sc, string? msg, OperationResult or)
        {
            Success = sc;
            Message = msg;
            OpResult = or;
        }
        private ServiceResult(T val, OperationResult or)
        {
            Data = val;
            OpResult = or;
            Success = true;
            Message = string.Empty;
        }
        public static ServiceResult<T> Fail(string message, OperationResult code)
        {
            return new ServiceResult<T>(false, message, code);
        }
        public static ServiceResult<T> Ok(T data, OperationResult code = OperationResult.Success) 
        {
            return new ServiceResult<T>(data, code);
        }
    }

    public enum OperationResult
    {
        Success,
        AccessDenied,
        Forbidden,
        ObjectNotFound,
        ValidationFail,
        FailedAuth,
        CreationFailed
    }

}

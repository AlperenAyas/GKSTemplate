using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GKS.Application.Wrappers
{
    public class TResult<T> where T : class
    {
        public T Data { get; set; }
        public bool Success { get; set; }
        public int Code { get; set; }
        public string Message { get; set; }
        public TResult(T data,string message,bool success = true, int code = 100)
        {
            Data = data;
            Success = success;
            Code = code;
            Message = message;
        }
    }
}

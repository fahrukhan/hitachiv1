using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hitachiv1.Models
{
    public class Response<T>
    {
        public T? Data {get; set;}
        public bool Success { get; set; } = true;
        public string Message { get; set; } = string.Empty;
    }
}
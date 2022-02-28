using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI_SAMPLE.Model
{
    public class ServiceResponse<T>
    {
        public T Data { get; set; }

        public bool Result { get; set; } = false;

        public string Message { get; set; } = null;
    }
}

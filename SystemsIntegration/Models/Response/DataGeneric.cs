using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SystemsIntegration.Api.Models.Response
{
    public class DataGeneric<T>
    {
        public T Data { get; set; }
    }
}

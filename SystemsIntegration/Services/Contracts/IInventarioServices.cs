using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SystemsIntegration.Api.Services.Contracts
{
    public interface IInventarioServices
    {
        public Task</*int codCliente, string mensaje,*/ List<(string, string)>> ObtieneProductosPorActualizar();
    }
}

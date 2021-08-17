using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SystemsIntegration.Api.DataAccess.Contracts
{
    public interface IInventarioContext
    {
        public Task</*int codCliente, string mensaje,*/ List<(string, string)>> ObtieneProductosPorActualizar();
    }
}

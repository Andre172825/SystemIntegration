using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SystemsIntegration.Api.DataAccess.Contracts;
using SystemsIntegration.Api.DataAccess.Integracions;
using SystemsIntegration.Api.Services.Contracts;

namespace SystemsIntegration.Api.Services.Implementation
{
    public class InventarioServices : IInventarioServices
    {
        //private readonly IInventarioServices _inventarioServices;
        private readonly IInventarioContext _inventarioContext;
        public InventarioServices(IInventarioContext inventarioContext)
        {
            //_inventarioServices = inventarioServices;
            _inventarioContext = inventarioContext;
        }
        public InventarioServices() { }

        public async Task</*int codCliente, string mensaje,*/ List<(string, string)>> ObtieneProductosPorActualizar()
        {
            /*int codCliente, string mensaje,*/ List<(string, string)> result;
            result = await _inventarioContext.ObtieneProductosPorActualizar();
            /*
             validación con el codCliente
             */
            return result;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JSBankApi.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JSBankApi.Core.Interfaces
{
    public interface IClienteService
    {
        Task<Cliente> CrearCliente(Cliente cliente);
        Task<Cliente?> ObtenerClientePorId(int id);
        Task<IEnumerable<Cliente>> ObtenerTodosLosClientes();
    }
}

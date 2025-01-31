using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JSBankApi.Core.Entities;
using JSBankApi.Core.Interfaces;
using JSBankApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JSBankApi.Infrastructure.Services
{
    public class ClienteService : IClienteService
    {
        private readonly AppDbContext _context;

        public ClienteService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Cliente> CrearCliente(Cliente cliente)
        {
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();
            return cliente;
        }

        public async Task<Cliente?> ObtenerClientePorId(int id)
        {
            var cliente = await _context.Clientes
            .Include(c => c.Cuentas)
            .FirstOrDefaultAsync(c => c.Id == id);

            if (cliente == null)
            {
                throw new KeyNotFoundException("El cliente no existe.");
            }

            return cliente;
        }

        public async Task<IEnumerable<Cliente>> ObtenerTodosLosClientes()
        {
            return await _context.Clientes.ToListAsync();
        }
    }
}

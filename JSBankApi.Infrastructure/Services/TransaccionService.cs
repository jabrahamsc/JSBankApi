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
    public class TransaccionService : ITransaccionService
    {
        private readonly AppDbContext _context;

        public TransaccionService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Transaccion> RealizarDeposito(int cuentaId, decimal monto)
        {
            var cuenta = await _context.CuentasBancarias.FindAsync(cuentaId);
            if (cuenta == null)
                throw new KeyNotFoundException("Cuenta no encontrada");

            cuenta.Saldo += monto;

            var transaccion = new Transaccion
            {
                Tipo = "Deposito",
                Monto = monto,
                Fecha = DateTime.UtcNow,
                CuentaBancariaId = cuentaId
            };

            _context.Transacciones.Add(transaccion);
            await _context.SaveChangesAsync();

            return transaccion;
        }

        public async Task<Transaccion> RealizarRetiro(int cuentaId, decimal monto)
        {
            var cuenta = await _context.CuentasBancarias.FindAsync(cuentaId);
            if (cuenta == null)
                throw new KeyNotFoundException("Cuenta no encontrada");

            if (cuenta.Saldo < monto)
                throw new InvalidOperationException("Fondos insuficientes");

            cuenta.Saldo -= monto;

            var transaccion = new Transaccion
            {
                Tipo = "Retiro",
                Monto = monto,
                Fecha = DateTime.UtcNow,
                CuentaBancariaId = cuentaId
            };

            _context.Transacciones.Add(transaccion);
            await _context.SaveChangesAsync();

            return transaccion;
        }

        public async Task<IEnumerable<Transaccion>> ObtenerHistorialTransacciones(int cuentaId)
        {
            return await _context.Transacciones
                .Where(t => t.CuentaBancariaId == cuentaId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Transaccion>> ObtenerHistorialTransaccionesPorNumeroCuenta(string numeroCuenta)
        {
            var cuenta = await _context.CuentasBancarias
                .FirstOrDefaultAsync(c => c.NumeroCuenta == numeroCuenta);

            if (cuenta == null)
            {
                throw new KeyNotFoundException("Cuenta no encontrada.");
            }

            var transacciones = await _context.Transacciones
                .Where(t => t.CuentaBancariaId == cuenta.Id)
                .ToListAsync();

            return transacciones;
        }
    }
}

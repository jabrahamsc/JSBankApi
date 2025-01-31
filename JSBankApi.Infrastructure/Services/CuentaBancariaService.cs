using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JSBankApi.Core.Entities;
using JSBankApi.Core.Interfaces;
using JSBankApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace JSBankApi.Infrastructure.Services
{
    public class CuentaBancariaService : ICuentaBancariaService
    {
        private readonly AppDbContext _context;

        public CuentaBancariaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<CuentaBancaria> CrearCuentaBancaria(CuentaBancaria cuenta)
        {
            //Validar si el numero de cuenta a ingresar ya esta registrado en la bd
            var cuentaExistente = await _context.CuentasBancarias
                .FirstOrDefaultAsync(c => c.NumeroCuenta == cuenta.NumeroCuenta);
            if (cuentaExistente != null)
            {
                throw new InvalidOperationException("El numero de cuenta ya esta registrado.");
            }
            else
            {
                _context.CuentasBancarias.Add(cuenta);
                await _context.SaveChangesAsync();
                return cuenta;
            }
            
        }

        public async Task<decimal> ConsultarSaldo(int cuentaId)
        {
            var cuenta = await _context.CuentasBancarias.FindAsync(cuentaId);
            return cuenta?.Saldo ?? 0;
        }

        public async Task<decimal> ConsultarSaldoPorNumeroCuenta(string numeroCuenta)
        {
            var cuenta = await _context.CuentasBancarias
                .FirstOrDefaultAsync(c => c.NumeroCuenta == numeroCuenta);

            if (cuenta == null)
            {
                throw new KeyNotFoundException("Cuenta no encontrada.");
            }

            return cuenta.Saldo;
        }
        public async Task<IEnumerable<CuentaBancaria>> ObtenerTodasLasCuentasRegistradas()
        {
            return await _context.CuentasBancarias.ToListAsync();
        }

        public async Task<CuentaBancaria> AplicarIntereses(string numeroCuenta, decimal tasaInteres)
        {
            if (tasaInteres <= 0)
            {
                throw new ArgumentException("La tasa de interés debe ser mayor que 0.");
            }

            var cuenta = await _context.CuentasBancarias
                .FirstOrDefaultAsync(c => c.NumeroCuenta == numeroCuenta);

            if (cuenta == null)
            {
                throw new KeyNotFoundException("La cuenta no existe.");
            }

            decimal intereses = cuenta.Saldo * tasaInteres;
            cuenta.Saldo += intereses;
            var transaccion = new Transaccion
            {
                Tipo = "Interes",
                Monto = intereses,
                Fecha = DateTime.UtcNow,
                CuentaBancariaId = cuenta.Id
            };

            _context.Transacciones.Add(transaccion);
            await _context.SaveChangesAsync();

            return cuenta;
        }
    }
}

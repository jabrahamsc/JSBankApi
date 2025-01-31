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
    public interface ITransaccionService
    {
        Task<Transaccion> RealizarDeposito(string numeroCuenta, decimal monto);
        Task<Transaccion> RealizarRetiro(string numeroCuenta, decimal monto);
        Task<IEnumerable<Transaccion>> ObtenerHistorialTransacciones(int cuentaId);
        Task<IEnumerable<Transaccion>> ObtenerHistorialTransaccionesPorNumeroCuenta(string NumeroCuenta);
    }
}

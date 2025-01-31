using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JSBankApi.Core.Entities;
using System.Threading.Tasks;

namespace JSBankApi.Core.Interfaces
{
    public interface ICuentaBancariaService
    {
        Task<CuentaBancaria> CrearCuentaBancaria(CuentaBancaria cuenta);
        Task<decimal> ConsultarSaldo(int cuentaId);
        Task<decimal> ConsultarSaldoPorNumeroCuenta(string numeroCuenta);
        Task<IEnumerable<CuentaBancaria>> ObtenerTodasLasCuentasRegistradas();
        Task<CuentaBancaria> AplicarIntereses(string numeroCuenta, decimal tasaInteres);
    }
}

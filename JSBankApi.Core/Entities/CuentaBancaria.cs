using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace JSBankApi.Core.Entities
{
    public class CuentaBancaria
    {
        public int Id { get; set; }
        public required string NumeroCuenta { get; set; }
        public decimal Saldo { get; set; }
        public int ClienteId { get; set; }
        public Cliente? Cliente { get; set; }
        public ICollection<Transaccion> Transacciones { get; set; } = new List<Transaccion>();
    }
}

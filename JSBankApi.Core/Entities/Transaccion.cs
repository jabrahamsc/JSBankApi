using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSBankApi.Core.Entities
{
    public class Transaccion
    {
        public int Id { get; set; }
        public string? Tipo { get; set; }
        public decimal Monto { get; set; }
        public DateTime Fecha { get; set; }
        public int CuentaBancariaId { get; set; }
        public CuentaBancaria? CuentaBancaria { get; set; }

    }
}


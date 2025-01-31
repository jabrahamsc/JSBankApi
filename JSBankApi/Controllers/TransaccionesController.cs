using Microsoft.AspNetCore.Http;
using JSBankApi.Core.Entities;
using JSBankApi.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JSBankApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransaccionesController : ControllerBase
    {
        private readonly ITransaccionService _transaccionService;

        public TransaccionesController(ITransaccionService transaccionService)
        {
            _transaccionService = transaccionService;
        }

        [HttpPost("deposito")]
        public async Task<IActionResult> RealizarDeposito(int cuentaId, decimal monto)
        {
            var transaccion = await _transaccionService.RealizarDeposito(cuentaId, monto);
            return Ok(transaccion);
        }

        [HttpPost("retiro")]
        public async Task<IActionResult> RealizarRetiro(int cuentaId, decimal monto)
        {
            var transaccion = await _transaccionService.RealizarRetiro(cuentaId, monto);
            return Ok(transaccion);
        }

        [HttpGet("{cuentaId}/historial")]
        public async Task<IActionResult> ObtenerHistorialTransacciones(int cuentaId)
        {
            var transacciones = await _transaccionService.ObtenerHistorialTransacciones(cuentaId);
            return Ok(transacciones);
        }

        [HttpGet("historial/{numeroCuenta}")]
        public async Task<IActionResult> ObtenerHistorialTransaccionesPorNumeroCuenta(string numeroCuenta)
        {
            try
            {
                var transacciones = await _transaccionService.ObtenerHistorialTransaccionesPorNumeroCuenta(numeroCuenta);
                return Ok(transacciones);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocurrio un error en el servidor, favor validar los datos ingresados");
            }
        }
    }
}

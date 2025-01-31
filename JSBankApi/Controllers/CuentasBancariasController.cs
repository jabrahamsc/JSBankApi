using Microsoft.AspNetCore.Http;
using JSBankApi.Core.Entities;
using JSBankApi.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace JSBankApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuentasBancariasController : ControllerBase
    {
        private readonly ICuentaBancariaService _cuentaBancariaService;

        public CuentasBancariasController(ICuentaBancariaService cuentaBancariaService)
        {
            _cuentaBancariaService = cuentaBancariaService;
        }

        [HttpPost]
        public async Task<IActionResult> CrearCuentaBancaria([FromBody] CuentaBancaria cuenta)
        {
            if (cuenta == null || !ModelState.IsValid)
            {
                return BadRequest("Datos de la cuenta no válidos.");
            }

            try
            {
                var nuevaCuenta = await _cuentaBancariaService.CrearCuentaBancaria(cuenta);
                return Ok(nuevaCuenta);
            }catch(InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }catch (Exception ex)
            {
                return StatusCode(500, "Ocurrio un error interno en el servidor, verifique los datos ingresados");
            }

        }

        [HttpGet("{id}/saldo")]
        public async Task<IActionResult> ConsultarSaldo(int id)
        {
            var saldo = await _cuentaBancariaService.ConsultarSaldo(id);
            return Ok(saldo);
        }

        [HttpGet("saldo/{numeroCuenta}")]
        public async Task<IActionResult> ConsultarSaldoPorNumeroCuenta(string numeroCuenta)
        {
            try
            {
                var saldo = await _cuentaBancariaService.ConsultarSaldoPorNumeroCuenta(numeroCuenta);
                return Ok(new { Saldo = saldo });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocurrio un error interno en el servidor.");
            }
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodasLasCuentasRegistradas()
        {
            var cuentas = await _cuentaBancariaService.ObtenerTodasLasCuentasRegistradas();
            return Ok(cuentas);
        }

        [HttpPost("aplicar-intereses")]
        public async Task<IActionResult> AplicarIntereses(string numeroCuenta, decimal tasaInteres)
        {

            try
            {
                var cuenta = await _cuentaBancariaService.AplicarIntereses(numeroCuenta, tasaInteres);
                return Ok(cuenta);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocuurrio un error en el servidor, por favor verifique los datos ingresados.");
            }
        }
    }
}

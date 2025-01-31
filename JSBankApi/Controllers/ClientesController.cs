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
    public class ClientesController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public ClientesController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpPost]
        public async Task<IActionResult> CrearCliente(Cliente cliente)
        {
            var nuevoCliente = await _clienteService.CrearCliente(cliente);
            return Ok(nuevoCliente);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerClientePorId(int id)
        {
            try
            {
                var cliente = await _clienteService.ObtenerClientePorId(id);
                return Ok(cliente);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocurrio un error en el servidor, por favor valide los datos imgresados");
            }
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodosLosClientes()
        {
            var clientes = await _clienteService.ObtenerTodosLosClientes();
            return Ok(clientes);
        }
    }
}

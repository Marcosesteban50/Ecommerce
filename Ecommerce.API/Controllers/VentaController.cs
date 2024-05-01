using Ecommerce.DTO;
using Ecommerce.Servicio.Contrato;
using Ecommerce.Servicio.Implementacion;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentaController : Controller
    {
        private readonly IVentaServicio ventaServicio;

        public VentaController(IVentaServicio ventaServicio)
        {
            this.ventaServicio = ventaServicio;
        }


        // ? significa que puede recibir nulos
        [HttpPost("Registrar")]
        //NA es por defecto en caso de que buscar no reciba nada
        public async Task<IActionResult> Registrar([FromBody] VentaDTO modelo)
        {

            var response = new ResponseDTO<VentaDTO>();


            try
            {
                



                response.EsCorrecto = true;
                response.Resultado = await ventaServicio.Registrar(modelo);


            }
            catch (Exception ex)
            {

                response.EsCorrecto = false;
                response.Mensaje = ex.Message;
            }

            return Ok(response);


        }


    }
}

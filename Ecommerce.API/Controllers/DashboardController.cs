using Ecommerce.DTO;
using Ecommerce.Servicio.Contrato;
using Ecommerce.Servicio.Implementacion;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : Controller
    {
        private readonly IDashboardServicio dashboardServicio;

        public DashboardController(IDashboardServicio dashboardServicio)
        {
            this.dashboardServicio = dashboardServicio;
        }



        [HttpGet("Resumen")]

        public  IActionResult Resumen()
        {

            var response = new ResponseDTO<DashboardDTO>();


            try
            {


                response.EsCorrecto = true;
                response.Resultado =  dashboardServicio.Resumen();


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

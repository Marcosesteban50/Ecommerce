using Ecommerce.DTO;
using Ecommerce.Servicio.Contrato;
using Ecommerce.Servicio.Implementacion;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : Controller
    {
        private readonly IProductoServicio productoServicio;

        public ProductoController(IProductoServicio productoServicio)
        {
            this.productoServicio = productoServicio;
        }





        // ? significa que puede recibir nulos
        [HttpGet("Lista/{buscar:alpha?}")]
        //NA es por defecto en caso de que buscar no reciba nada
        public async Task<IActionResult> Lista(string buscar = "NA")
        {

            var response = new ResponseDTO<List<ProductoDTO>>();


            try
            {
                if (buscar == "NA") buscar = "";



                response.EsCorrecto = true;
                response.Resultado = await productoServicio.Lista(buscar);


            }
            catch (Exception ex)
            {

                response.EsCorrecto = false;
                response.Mensaje = ex.Message;
            }

            return Ok(response);


        }




        // ? significa que puede recibir nulos
        [HttpGet("Catalogo/{categoria}/{buscar?}")]
        //NA es por defecto en caso de que buscar no reciba nada
        public async Task<IActionResult> Catalogo(string categoria,string buscar = "NA")
        {

            var response = new ResponseDTO<List<ProductoDTO>>();


            try
            {
                if (categoria.ToLower() == "todos") categoria = "";
                if (buscar == "NA") buscar = "";



                response.EsCorrecto = true;
                response.Resultado = await productoServicio.Catalogo(categoria,buscar);


            }
            catch (Exception ex)
            {

                response.EsCorrecto = false;
                response.Mensaje = ex.Message;
            }

            return Ok(response);


        }






        // ? significa que puede recibir nulos
        [HttpGet("Obtener/{id:int}")]
        //NA es por defecto en caso de que buscar no reciba nada
        public async Task<IActionResult> Obtener(int id)
        {

            var response = new ResponseDTO<ProductoDTO>();


            try
            {
            



                response.EsCorrecto = true;
                response.Resultado = await productoServicio.Obtener(id);


            }
            catch (Exception ex)
            {

                response.EsCorrecto = false;
                response.Mensaje = ex.Message;
            }

            return Ok(response);


        }





        // ? significa que puede recibir nulos
        [HttpPost("Crear")]
        //NA es por defecto en caso de que buscar no reciba nada
        public async Task<IActionResult> Crear([FromBody] ProductoDTO modelo)
        {

            var response = new ResponseDTO<ProductoDTO>();


            try
            {




                response.EsCorrecto = true;
                response.Resultado = await productoServicio.Crear(modelo);


            }
            catch (Exception ex)
            {

                response.EsCorrecto = false;
                response.Mensaje = ex.Message;
            }

            return Ok(response);


        }





        // ? significa que puede recibir nulos
        [HttpPut("Editar")]
        //NA es por defecto en caso de que buscar no reciba nada
        public async Task<IActionResult> Editar([FromBody] ProductoDTO modelo)
        {

            var response = new ResponseDTO<bool>();


            try
            {




                response.EsCorrecto = true;
                response.Resultado = await productoServicio.Editar(modelo);


            }
            catch (Exception ex)
            {

                response.EsCorrecto = false;
                response.Mensaje = ex.Message;
            }

            return Ok(response);


        }





        // ? significa que puede recibir nulos
        [HttpDelete("Eliminar/{id:int}")]
        //NA es por defecto en caso de que buscar no reciba nada
        public async Task<IActionResult> Eliminar(int id)
        {

            var response = new ResponseDTO<bool>();


            try
            {




                response.EsCorrecto = true;
                response.Resultado = await productoServicio.Eliminar(id);


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

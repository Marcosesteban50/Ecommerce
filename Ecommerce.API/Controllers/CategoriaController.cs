using AutoMapper;
using Ecommerce.DTO;
using Ecommerce.Servicio.Contrato;
using Ecommerce.Servicio.Implementacion;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaServicio categoriaServicio;

        public CategoriaController(ICategoriaServicio categoriaServicio)
        {
            this.categoriaServicio = categoriaServicio;
        }



        // ? significa que puede recibir nulos
        [HttpGet("Lista/{buscar?}")]
        //NA es por defecto en caso de que buscar no reciba nada
        public async Task<IActionResult> Lista(string buscar = "NA")
        {

            var response = new ResponseDTO<List<CategoriaDTO>>();


            try
            {
                if (buscar == "NA") buscar = "";



                response.EsCorrecto = true;
                response.Resultado = await categoriaServicio.Lista(buscar);


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

            var response = new ResponseDTO<CategoriaDTO>();


            try
            {
               


                response.EsCorrecto = true;
                response.Resultado = await categoriaServicio.Obtener(id);


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
        public async Task<IActionResult> Crear([FromBody]CategoriaDTO modelo)
        {

            var response = new ResponseDTO<CategoriaDTO>();


            try
            {
                



                response.EsCorrecto = true;
                response.Resultado = await categoriaServicio.Crear(modelo);


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
        public async Task<IActionResult> Editar([FromBody] CategoriaDTO modelo)
        {

            var response = new ResponseDTO<bool>();


            try
            {




                response.EsCorrecto = true;
                response.Resultado = await categoriaServicio.Editar(modelo);


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
                response.Resultado = await categoriaServicio.Eliminar(id);


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

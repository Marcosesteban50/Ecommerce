﻿using Ecommerce.DTO;
using Ecommerce.Servicio.Contrato;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Ecommerce.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioServicio usuarioServicio;
       

        public UsuarioController(IUsuarioServicio usuarioServicio
         )
        {
            this.usuarioServicio = usuarioServicio;
            
        }






        // ? significa que puede recibir nulos
        [HttpGet("Lista/{rol:alpha}/{buscar:alpha?}")]
                                              //NA es por defecto en caso de que buscar no reciba nada
        public async Task<IActionResult> Lista(string rol,string buscar = "NA")
        {

            var response = new ResponseDTO<List<UsuarioDTO>>();


            try
            {
                if (buscar == "NA")buscar = "";
                
                    
                
                response.EsCorrecto = true;
                response.Resultado = await usuarioServicio.Lista(rol, buscar);


        }catch(Exception ex)
            {
                
                response.EsCorrecto = false;
                response.Mensaje = ex.Message;
            }

            return Ok(response);


        }

       
        [HttpGet("Obtener/{id:int}")]
      
        public async Task<IActionResult> Obtener(int id)
        {

            var response = new ResponseDTO<UsuarioDTO>();


            try
            {
                
                response.EsCorrecto = true;
                response.Resultado = await usuarioServicio.Obtener(id);


            }
            catch (Exception ex)
            {
                response.EsCorrecto = false;
                response.Mensaje = ex.Message;
            }

            return Ok(response);


        }




        
        [HttpPost("Crear")]

        /*
         
    [FromBody], indicas explícitamente que el 
        objeto modelo debe obtenerse del cuerpo de la solicitud.
         */

        public async Task<IActionResult> Crear([FromBody]UsuarioDTO modelo)
        {

            var response = new ResponseDTO<UsuarioDTO>();


            try
            {
                

                response.EsCorrecto = true;
                response.Resultado = await usuarioServicio.Crear(modelo);


            }
            catch (Exception ex)
            {
                response.EsCorrecto = false;
                response.Mensaje = ex.Message;
            }

            return Ok(response);


        }



       
        [HttpPost("Autorizacion")]
       
        public async Task<IActionResult> Autorizacion([FromBody] LoginDTO modelo)
        {

            var response = new ResponseDTO<SesionDTO>();


            try
            {


                response.EsCorrecto = true;
                response.Resultado = await usuarioServicio.Autorizacion(modelo);


            }
            catch (Exception ex)
            {
                response.EsCorrecto = false;
                response.Mensaje = ex.Message;
            }

            return Ok(response);


        }





        [HttpPut("Editar")]
       
        public async Task<IActionResult> Editar([FromBody] UsuarioDTO modelo)
        {

            var response = new ResponseDTO<bool>();


            try
            {


                response.EsCorrecto = true;
                response.Resultado = await usuarioServicio.Editar(modelo);


            }
            catch (Exception ex)
            {
                response.EsCorrecto = false;
                response.Mensaje = ex.Message;
            }

            return Ok(response);


        }





        [HttpDelete("Eliminar/{id:int}")]
        //NA es por defecto en caso de que buscar no reciba nada
        public async Task<IActionResult> Eliminar(int id)
        {

            var response = new ResponseDTO<bool>();


            try
            {


                response.EsCorrecto = true;
                response.Resultado = await usuarioServicio.Eliminar(id);


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

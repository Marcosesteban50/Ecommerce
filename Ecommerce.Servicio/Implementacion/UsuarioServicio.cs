using AutoMapper;
using Ecommerce.DTO;
using Ecommerce.Modelo;
using Ecommerce.Repositorio.Contrato;
using Ecommerce.Servicio.Contrato;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Servicio.Implementacion
{
    public class UsuarioServicio : IUsuarioServicio
    {
        private readonly IGenericoRepositorio<Usuario> modeloRepositorio;
        private readonly IMapper mapper;

        public UsuarioServicio(IGenericoRepositorio<Usuario> modeloRepositorio,
            IMapper mapper)
        {
            this.modeloRepositorio = modeloRepositorio;
            this.mapper = mapper;
        }

        public async Task<SesionDTO> Autorizacion(LoginDTO modelo)
        {
            try
            {
                //Esto Develve un Iquereable osea una Query como un Select * en SQL
                var consulta = modeloRepositorio.
                    Consultar(p => p.Correo == modelo.Correo
                    && p.Clave == modelo.Clave);

                //Tomando Primer resultado encontrado en la consulta
                var fromDbModelo = await consulta.FirstOrDefaultAsync();

                //Existe Usuario con Ese Correo y Clave
                if (fromDbModelo != null)
                {

                    //fromDbModelo -> SesionDTO
                    return mapper.Map<SesionDTO>(fromDbModelo);
                }
                else
                {
                    //Cancelando Tarea si no se encuentra nada
                    //Esto brincara a la exepcion de abajo ojo
                    throw new TaskCanceledException("No se encontraron coincidencias");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<UsuarioDTO> Crear(UsuarioDTO modelo)
        {
            try
            {
                //modelo -> Usuario
                var dbModelo = mapper.Map<Usuario>(modelo);

                //creando el Modelo de la base de datos
                var rspModelo = await modeloRepositorio.Crear(dbModelo);


                //Si es diferente de 0 significa que si lo creo
                if (rspModelo.IdUsuario != 0)
                {

                    //rspModelo -> UsuarioDTO
                    return mapper.Map<UsuarioDTO>(rspModelo);
                }
                else
                {
                    //y esto brincara a la exepcion de abajo ojo 
                    throw new TaskCanceledException("No se puede crear");
                }



            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Editar(UsuarioDTO modelo)
        {

            try
            {

                //Buscando un Usuario Similar al que recibimos en el Modelo
                var consulta = modeloRepositorio.Consultar(p => p.IdUsuario == modelo.IdUsuario);

                //Tomando Primer resultado encontrado en la consulta

                var fromDbModelo = await consulta.FirstOrDefaultAsync();

                //Si No es null entonces existe
                if (fromDbModelo != null)
                {
                    fromDbModelo.NombreCompleto = modelo.NombreCompleto;
                    fromDbModelo.Correo = modelo.Correo;
                    fromDbModelo.Clave = modelo.Clave;

                    //Editando el modelo desde la base de datos
                    var rsp = await modeloRepositorio.Editar(fromDbModelo);


                    //Si es falso, osea si no se pudo editar
                    if (!rsp)
                    {
                        throw new TaskCanceledException("No se pudo editar");
                    }

                    //retornamos la respuesta rsp
                 return rsp;


                    //-------------------------
                }
                else
                {
                    throw new TaskCanceledException("No se encontraron resultados");

                }



            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Eliminar(int id)
        {
            try
            {
                //Buscando un Usuario Similar al que recibimos en el Modelo
                var consulta = modeloRepositorio.Consultar(p => p.IdUsuario == id);

                //Tomando Primer resultado encontrado en la consulta

                var fromDbModelo = await consulta.FirstOrDefaultAsync();

                //Si No es null entonces encontro almenos 1 usuario
                //
                if (fromDbModelo != null)
                {
                    var rsp = await modeloRepositorio.Eliminar(fromDbModelo);


                    //Si es falso, osea si no se pudo borrar

                    if (!rsp)
                    {
                        throw new TaskCanceledException("No se pudo eliminar");
                       
                    }
                    //retornamos la respuesta rsp
                    return rsp;
                }
                else
                {
                    throw new TaskCanceledException("No se encontraron resultados");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<UsuarioDTO>> Lista(string rol, string buscar)
        {
            try
            {
                var consulta = modeloRepositorio.
                    Consultar(p => p.Rol ==  rol && 
                    //EL ! ahi es que confiamos que no sera nulo :)
                    //Concatenamos el Nombre, y Correo para ver si encuentra lo que busca
                    //en esos tambien
                    string.Concat(p.NombreCompleto!.ToLower(),p.Correo!.ToLower()).Contains
                    (buscar.ToLower()));


                //consulta -> Lista<UsuarioDTO>
                List<UsuarioDTO> lista = mapper.Map<List<UsuarioDTO>>(await consulta.ToListAsync());

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<UsuarioDTO> Obtener(int id)
        {
            try
            {
                
                //Buscando un Usuario Similar al que recibimos en el Modelo
                var consulta = modeloRepositorio.Consultar(p => p.IdUsuario == id);

                //Tomando Primer resultado encontrado en la consulta

                var fromDbModelo = await consulta.FirstOrDefaultAsync();

                //Si No es null entonces encontro almenos 1 usuario
                if (fromDbModelo != null)
                {
                    //fromDbModelo -> UsuarioDTO
                    return mapper.Map<UsuarioDTO>(fromDbModelo);
                }
                else
                {
                    throw new TaskCanceledException("No se encontraron coincidencias");
                }

                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

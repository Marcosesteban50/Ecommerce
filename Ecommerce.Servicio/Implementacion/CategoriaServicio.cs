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
    public  class CategoriaServicio  : ICategoriaServicio
    {
        private readonly IGenericoRepositorio<Categoria> modeloRepositorio;
        private readonly IMapper mapper;

        public CategoriaServicio(IGenericoRepositorio<Categoria> modeloRepositorio,
            IMapper mapper)
        {
            this.modeloRepositorio = modeloRepositorio;
            this.mapper = mapper;
        }

        public async Task<CategoriaDTO> Crear(CategoriaDTO modelo)
        {
            try
            {
                //modelo -> Usuario
                var dbModelo = mapper.Map<Categoria>(modelo);

                //creando el Modelo de la base de datos
                var rspModelo = await modeloRepositorio.Crear(dbModelo);


                //Si es diferente de 0 significa que si lo creo
                if (rspModelo.IdCategoria != 0)
                {

                    //rspModelo -> UsuarioDTO
                    return mapper.Map<CategoriaDTO>(rspModelo);
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

        public async Task<bool> Editar(CategoriaDTO modelo)
        {
            try
            {

                //Buscando un Usuario Similar al que recibimos en el Modelo
                var consulta = modeloRepositorio.Consultar(p => p.IdCategoria == modelo.IdCategoria);

                //Tomando Primer resultado encontrado en la consulta

                var fromDbModelo = await consulta.FirstOrDefaultAsync();

                //Si No es null entonces existe
                if (fromDbModelo != null)
                {
                    fromDbModelo.Nombre = modelo.Nombre;
                  

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
                var consulta = modeloRepositorio.Consultar(p => p.IdCategoria == id);

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

        public async Task<List<CategoriaDTO>> Lista(string valorAencontrar)
        {
            try
            {
                var consulta = modeloRepositorio.
                    Consultar(p =>
                    //EL ! ahi es que confiamos que no sera nulo :)
                    (p.Nombre!.ToLower()).Contains
                    (valorAencontrar.ToLower()));


                //consulta -> Lista<UsuarioDTO>
                List<CategoriaDTO> lista = mapper.Map<List<CategoriaDTO>>(await consulta.ToListAsync());

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<CategoriaDTO> Obtener(int id)
        {
            try
            {

                //Buscando un Usuario Similar al que recibimos en el Modelo
                var consulta = modeloRepositorio.Consultar(p => p.IdCategoria == id);

                //Tomando Primer resultado encontrado en la consulta

                var fromDbModelo = await consulta.FirstOrDefaultAsync();

                //Si No es null entonces encontro almenos 1 usuario
                if (fromDbModelo != null)
                {
                    //fromDbModelo -> UsuarioDTO
                    return mapper.Map<CategoriaDTO>(fromDbModelo);
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

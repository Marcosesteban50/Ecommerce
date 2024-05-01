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
    public  class ProductoServicio : IProductoServicio
    {
        private readonly IGenericoRepositorio<Producto> modeloRepositorio;
        private readonly IMapper mapper;

        public ProductoServicio(IGenericoRepositorio<Producto> modeloRepositorio,
            IMapper mapper)
        {
            this.modeloRepositorio = modeloRepositorio;
            this.mapper = mapper;
        }

        public async Task<List<ProductoDTO>> Catalogo(string categoria, string buscar)
        {
            try
            {
                var consulta = modeloRepositorio.
                    Consultar(p => p.Nombre!.ToLower().Contains(buscar.ToLower())
                    //EL ! ahi es que confiamos que no sera nulo :)
                    && p.IdCategoriaNavigation!.Nombre!.ToLower().Contains(categoria.ToLower()));


                //consulta -> Lista<UsuarioDTO>
                List<ProductoDTO> lista = mapper.Map<List<ProductoDTO>>(await consulta.ToListAsync());

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ProductoDTO> Crear(ProductoDTO modelo)
        {
            try
            {
                //modelo -> Usuario
                var dbModelo = mapper.Map<Producto>(modelo);

                //creando el Modelo de la base de datos
                var rspModelo = await modeloRepositorio.Crear(dbModelo);


                //Si es diferente de 0 significa que si lo creo
                if (rspModelo.IdProducto != 0)
                {

                    //rspModelo -> UsuarioDTO
                    return mapper.Map<ProductoDTO>(rspModelo);
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

        public async Task<bool> Editar(ProductoDTO modelo)
        {
            try
            {

                //Buscando un Usuario Similar al que recibimos en el Modelo
                var consulta = modeloRepositorio.Consultar(p => p.IdProducto == modelo.IdProducto);

                //Tomando Primer resultado encontrado en la consulta

                var fromDbModelo = await consulta.FirstOrDefaultAsync();

                //Si No es null entonces existe
                if (fromDbModelo != null)
                {
                    fromDbModelo.Nombre = modelo.Nombre;
                    fromDbModelo.Descripcion = modelo.Descripcion;
                    fromDbModelo.IdCategoria = modelo.IdCategoria;
                    fromDbModelo.Precio = modelo.Precio;
                    fromDbModelo.PrecioOferta = modelo.PrecioOferta;
                    fromDbModelo.Cantidad = modelo.Cantidad;
                    fromDbModelo.Imagen = modelo.Imagen;

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
                var consulta = modeloRepositorio.Consultar(p => p.IdProducto == id);

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

        public async Task<List<ProductoDTO>> Lista(string valorBuscado)
        {
            try
            {
                
                var consulta = modeloRepositorio.
                    Consultar(p => p.Nombre!.Contains       
                    (valorBuscado.ToLower()));

                //haciendo un innerjoin con la tabla de categoria
                consulta = consulta.Include(r => r.IdCategoriaNavigation);


                //consulta -> Lista<UsuarioDTO>
                List<ProductoDTO> lista = mapper.Map<List<ProductoDTO>>(await consulta.ToListAsync());

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ProductoDTO> Obtener(int id)
        {
            try
            {

                //Buscando un Usuario Similar al que recibimos en el Modelo
                var consulta = modeloRepositorio.Consultar(p => p.IdProducto == id);

                //haciendo un innerjoin con la tabla de categoria
                
                consulta = consulta.Include(r => r.IdCategoriaNavigation);
                
                //Tomando Primer resultado encontrado en la consulta

                var fromDbModelo = await consulta.FirstOrDefaultAsync();




                //Si No es null entonces encontro almenos 1 usuario
                if (fromDbModelo != null)
                {
                    //fromDbModelo -> UsuarioDTO
                    return mapper.Map<ProductoDTO>(fromDbModelo);
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

using Ecommerce.DTO;
using Ecommerce.Modelo;
using Ecommerce.Repositorio.Contrato;
using Ecommerce.Servicio.Contrato;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Servicio.Implementacion
{
    public class DashboardServicio : IDashboardServicio
    {
        private readonly IVentaRepositorio ventaRepositorio;
        private readonly IGenericoRepositorio<Usuario> usuarioRepositorio;
        private readonly IGenericoRepositorio<Producto> productoRepositorio;

        public DashboardServicio(IVentaRepositorio ventaRepositorio,
            IGenericoRepositorio<Usuario> usuarioRepositorio,
            IGenericoRepositorio<Producto> productoRepositorio)
        {
            this.ventaRepositorio = ventaRepositorio;
            this.usuarioRepositorio = usuarioRepositorio;
            this.productoRepositorio = productoRepositorio;
        }

        private string Ingresos()
        {

            //select from * a tabla de ventas
            var consulta = ventaRepositorio.Consultar();


            //sumando la columna de total y guardandola en ingresos
            decimal? ingresos = consulta.Sum(x => x.Total);


            //convirtiendo ingresos en string 
            return Convert.ToString(ingresos)!;
        }

        private int Ventas()
        {

            //select from * a tabla de ventas
            var consulta = ventaRepositorio.Consultar();


            //obteniendo el total de ventas 
            int totalVentas = consulta.Count();

            return totalVentas;
        }



        private int Clientes()
        {

            //select from * a tabla de usuarios con el Rol de cliente
            var consulta = usuarioRepositorio.Consultar(x => x.Rol!.ToLower() == "cliente");


            //obteniendo el total de clientes 
            int clientes = consulta.Count();



            return clientes;
        }


        private int Productos()
        {

            //select from * a tabla de productos
            var consulta = productoRepositorio.Consultar();


            //obteniendo el total de productos 
            int productos = consulta.Count();



            return productos;
        }

        public DashboardDTO Resumen()
        {

            try
            {

                var dto = new DashboardDTO()
                {
                    TotalIngresos = Ingresos(),
                    TotalVentas = Ventas(),
                    TotalClientes = Clientes(),
                    TotalProductos = Productos()
                };


                return dto;


            }catch(Exception ex)
            {
                throw ex;
            }

        }
    }
}

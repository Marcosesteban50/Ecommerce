using AutoMapper;
using Ecommerce.DTO;
using Ecommerce.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Utilidades
{
    public class AutoMapperProfile : Profile
    {

        public AutoMapperProfile()
        {


            //Usuario -> a UsuarioDTO
            CreateMap<Usuario, UsuarioDTO>();

            //Usuario -> a SesionDTO

            CreateMap<Usuario, SesionDTO>();

            //UsuarioDTO -> a Usuario

            CreateMap<UsuarioDTO, Usuario>();

            //Categoria -> a CategoriaDTO
            CreateMap<Categoria, CategoriaDTO>();

            //CategoriaDTO -> a Categoria

            CreateMap<CategoriaDTO, Categoria>();

            //Producto -> a ProductoDTO


            CreateMap<Producto, ProductoDTO>();

            //ProductoDTO -> a Producto


            CreateMap<ProductoDTO, Producto>()
                //Para el Miembro IdCategoriaNavigation 
                //opt Opciones variable local osea cualquier nombre que quieras
                

                //Ignoramos La propiedad de IdCategoriaNavigation en la conversion
                .ForMember(Destino => 
                Destino.IdCategoriaNavigation,
                opt => opt.Ignore()
                );


            //DetalleVenta -> DetalleVentaDTO
            CreateMap<DetalleVenta,DetalleVentaDTO>();

            //DetalleVentaDTO -> DetalleVenta

            CreateMap<DetalleVentaDTO, DetalleVenta>();



            //Venta -> VentaDTO
            CreateMap<Venta, VentaDTO>();


            //Venta -> VentaDTO
            CreateMap<VentaDTO, Venta>();

            //VentaDTO -> Venta

            CreateMap<DetalleVentaDTO, DetalleVenta>();




        }
    }
}

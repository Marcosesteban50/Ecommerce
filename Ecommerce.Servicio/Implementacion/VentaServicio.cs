using AutoMapper;
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
    public class VentaServicio : IVentaServicio
    {
        private readonly IVentaRepositorio modeloRepositorio;
        private readonly IMapper mapper;

        public VentaServicio(IVentaRepositorio modeloRepositorio,
            IMapper mapper)
        {
            this.modeloRepositorio = modeloRepositorio;
            this.mapper = mapper;
        }

        public async Task<VentaDTO> Registrar(VentaDTO modelo)
        {

            try
            {
                //modelo -> Usuario
                var dbModelo = mapper.Map<Venta>(modelo);

                //creando el Modelo de la base de datos
                var ventaGenerada = await modeloRepositorio.Registrar(dbModelo);


                //Si es igual a 0 significa que no se genero ni una sola venta
                if (ventaGenerada.IdVenta == 0)
                {

                    throw new TaskCanceledException("No se pudo registrar");
                  
                }

                //ventaGenerada -> VentaDTO
                return mapper.Map<VentaDTO>(ventaGenerada);
               



            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}

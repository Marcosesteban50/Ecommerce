using Ecommerce.Modelo;
using Ecommerce.Repositorio.Contrato;
using Ecommerce.Repositorio.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Repositorio.Implementacion
{
    public class VentaRepositorio : GenericoRepositorio<Venta>, IVentaRepositorio
    {
        private readonly DbecommerceContext dbContext;
        //base aqui es el generico
        public VentaRepositorio(DbecommerceContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Venta> Registrar(Venta modelo)
        {

            //Instanciando Clase de venta
            Venta ventaGenerada = new Venta();


            using(var transaction = dbContext.Database.BeginTransaction())
            {

                try
                {
                    foreach (DetalleVenta dv in modelo.DetalleVenta)
                    {
                        //Buscando el primer producto que encuentre
                        Producto productoEncontrado=  dbContext.Productos.Where(
                            p => p.IdProducto == dv.IdProducto ).First();

                        //restando cantidades en base a lo que tiene
                        productoEncontrado.Cantidad = productoEncontrado.Cantidad - dv.Cantidad;

                        //actualizando
                        dbContext.Productos.Update(productoEncontrado);

                    }


                        //guardando cambios
                        await dbContext.SaveChangesAsync();


                        //Agregando El Modelo de venta
                        await dbContext.Venta.AddAsync(modelo);


                        //guardando cambios nuevamente
                        await dbContext.SaveChangesAsync();
                        
                       //pasando el modelo de venta a la venta generada
                        ventaGenerada = modelo;

                    //Transacion exitosa asi que no tiene que restablecer ningun paso
                    //para mas info ve a https://chat.openai.com/c/411d4423-21b6-4cbb-b10f-be33e28c1f7c
                    transaction.Commit();
                }
                catch
                {
                    //si Hay Error hacemos un rollback osea revertir los cambios al estado anterior
                    transaction.Rollback();
                    throw;
                }


                //retornamos la venta generada
                return ventaGenerada;

            }
            

            
        }
    }
}

using Ecommerce.Repositorio.Contrato;
using Ecommerce.Repositorio.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Repositorio.Implementacion
{
    public class GenericoRepositorio<TModelo> : IGenericoRepositorio<TModelo> where TModelo : class 
    {
        private readonly DbecommerceContext dbContext;

        public GenericoRepositorio(DbecommerceContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IQueryable<TModelo> Consultar(Expression<Func<TModelo, bool>>? filtro = null)
        {
            //Creando Query como en SQL select * 
            IQueryable<TModelo> consulta = (filtro == null) ?
                dbContext.Set<TModelo>() : dbContext.Set<TModelo>().Where(filtro);

            return consulta;
        }

        public async Task<TModelo> Crear(TModelo modelo)
        {
            try
            {
                //Basicamente InsertInto a la tabla SQL
                dbContext.Set<TModelo>().Add(modelo);
                await dbContext.SaveChangesAsync();
                return modelo;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Editar(TModelo modelo)
        {
            try
            {
                //Basicamente Update a la tabla SQL
                dbContext.Set<TModelo>().Update(modelo);
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Eliminar(TModelo modelo)
        {
            try
            {
                //Basicamente Delete  a la tabla SQL
                dbContext.Set<TModelo>().Remove(modelo);
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw;
            }
        }
    }
    
}

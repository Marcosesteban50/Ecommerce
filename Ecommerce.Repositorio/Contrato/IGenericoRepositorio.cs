using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Repositorio.Contrato
{
    //<> = el modelo, lo que va dentro es el modelo
                                                  //TModelo es una clase
    public interface IGenericoRepositorio<TModelo> where TModelo : class
    {

        //Esto es como hacer un select * from usuario 
        IQueryable<TModelo> Consultar(Expression<Func<TModelo, bool>>? filtro = null);

        Task<TModelo> Crear(TModelo modelo);
        Task<bool> Editar(TModelo modelo);
        Task<bool> Eliminar(TModelo modelo);


    }
}

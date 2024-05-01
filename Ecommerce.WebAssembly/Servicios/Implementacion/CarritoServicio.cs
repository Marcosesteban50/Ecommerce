using Blazored.LocalStorage;
using Blazored.Toast.Services;
using Ecommerce.DTO;
using Ecommerce.WebAssembly.Servicios.Contrato;

namespace Ecommerce.WebAssembly.Servicios.Implementacion
{
    public class CarritoServicio : ICarritoServicio
    {
        private readonly ILocalStorageService localStorageService;
        private readonly ISyncLocalStorageService syncLocalStorageService;
        private readonly IToastService toastService;

        public CarritoServicio(ILocalStorageService localStorageService,
            ISyncLocalStorageService syncLocalStorageService,
            IToastService toastService)
        {
            this.localStorageService = localStorageService;
            this.syncLocalStorageService = syncLocalStorageService;
            this.toastService = toastService;
        }

        public event Action? MostrarItems;

        public async Task AgregarCarrito(CarritoDTO modelo)
        {

            try
            {

                //"carrito" ese carrito ahi es una key
                var carrito = await localStorageService.GetItemAsync<List<CarritoDTO>>("carrito");

                //No hemos agregado productos al carrito
                if(carrito == null)
                {
                    carrito = new List<CarritoDTO>();
                }

                var encontrado = carrito.FirstOrDefault(
                    //Producto sea igual al producto Modelo que recibimos
                    a => a.Producto!.IdProducto == modelo.Producto!.IdProducto);

                //Lo Encontro
                if(encontrado != null)
                {
                    carrito.Remove(encontrado);
                }

                carrito.Add(modelo);

                //"carrito" = llave , carrito = objeto creado mas arriba
                //actualizando el objeto carrito

                await localStorageService.SetItemAsync("carrito", carrito);


                if(encontrado != null)
                {
                    toastService.ShowSuccess("Producto fue actualizado en carrito");
                }
                else
                {
                    toastService.ShowSuccess("Producto fue agregado en carrito");

                }


                //Actualizando Vista
                MostrarItems!.Invoke();


            }
            catch
            {
                toastService.ShowError("No se pudo agregar al carrito!");

            }

        }

        public int CantidadProductos()
        {

            var carrito = syncLocalStorageService.GetItem<List<CarritoDTO>>("carrito");


            if (carrito == null)
            {
                return 0;
            }
            else
            {
                return carrito.Count();
            }
            

        }

        public async Task<List<CarritoDTO>> DevolverCarrito()
        {
            var carrito = await localStorageService.GetItemAsync<List<CarritoDTO>>("carrito");


            if(carrito == null)
            {
                carrito = new List<CarritoDTO>();
            }

            return carrito;
        }

        public async Task EliminarCarrito(int idProducto)
        {
            try
            {
            var carrito = await localStorageService.GetItemAsync<List<CarritoDTO>>("carrito");

                if(carrito != null)
                {
                    var elemento = carrito.FirstOrDefault(
                        x => x.Producto!.IdProducto == idProducto);

                    //existe el producto y necesitamos eliminar
                    if(elemento != null)
                    {
                        carrito.Remove(elemento);

                        //actualizando el objeto carrito
                        await localStorageService.SetItemAsync("carrito", carrito);

                        //mostrando vista o actualizando tambien
                        MostrarItems!.Invoke();
                    }
                }

            }
            catch
            {
                toastService.ShowError("No se pudo eliminar el producto del carrito!");

            }
        }

        public async Task LimpiarCarrito()
        {
            //Limpiando o borrando todo lo que esta en "carrito"
            await localStorageService.RemoveItemAsync("carrito");

            MostrarItems!.Invoke();


        }
    }
}

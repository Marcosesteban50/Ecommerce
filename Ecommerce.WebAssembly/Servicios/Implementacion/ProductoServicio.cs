using Ecommerce.DTO;
using Ecommerce.WebAssembly.Servicios.Contrato;
using System.Net.Http.Json;

namespace Ecommerce.WebAssembly.Servicios.Implementacion
{
    public class ProductoServicio : iProductoServicio


    {
        private readonly HttpClient httpClient;

        public ProductoServicio(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }




        public async Task<ResponseDTO<List<ProductoDTO>>> Catalogo(string categoria, string buscar)
        {
            return await httpClient.GetFromJsonAsync<ResponseDTO<List<ProductoDTO>>>($"Producto/Catalogo/{categoria}/{buscar}");

        }

        public async Task<ResponseDTO<ProductoDTO>> Crear(ProductoDTO modelo)
        {
            //PostAsJson("Controlador/Metodo")


            //Respuesta de haber ejecutado la API
            var response = await httpClient.PostAsJsonAsync("Producto/Crear", modelo);


            //Respuesta 
            var result = await response.Content.ReadFromJsonAsync<ResponseDTO<ProductoDTO>>();


            return result!;
        }

        public async Task<ResponseDTO<bool>> Editar(ProductoDTO modelo)
        {
            //PutAsJson("Controlador/Metodo")


            //Respuesta de haber ejecutado la API
            var response = await httpClient.PutAsJsonAsync("Producto/Editar", modelo);


            //Respuesta 
            var result = await response.Content.ReadFromJsonAsync<ResponseDTO<bool>>();


            return result!;
        }

        public async Task<ResponseDTO<bool>> Eliminar(int id)
        {

            //DeleteFromJsonAsync("Controlador/Metodo")


            //Respuesta de haber ejecutado la API
            return await httpClient.DeleteFromJsonAsync<ResponseDTO<bool>>($"Producto/Eliminar/{id}");
        }

        public async Task<ResponseDTO<List<ProductoDTO>>> Lista(string buscar)
        {
            return await httpClient.GetFromJsonAsync<ResponseDTO<List<ProductoDTO>>>($"Producto/Lista/{buscar}");

        }

        public async Task<ResponseDTO<ProductoDTO>> Obtener(int id)
        {
            return await httpClient.GetFromJsonAsync<ResponseDTO<ProductoDTO>>($"Producto/Obtener/{id}");

        }
    }
}

using Ecommerce.DTO;
using Ecommerce.WebAssembly.Servicios.Contrato;
using System.Net.Http.Json;

namespace Ecommerce.WebAssembly.Servicios.Implementacion
{
    public class CategoriaServicio : ICategoriaServicio
    {
        private readonly HttpClient httpClient;

        public CategoriaServicio(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<ResponseDTO<CategoriaDTO>> Crear(CategoriaDTO modelo)
        {
            //PostAsJson("Controlador/Metodo")


            //Respuesta de haber ejecutado la API
            var response = await httpClient.PostAsJsonAsync("Categoria/Crear", modelo);


            //Respuesta 
            var result = await response.Content.ReadFromJsonAsync<ResponseDTO<CategoriaDTO>>();


            return result!;
        }

        public async Task<ResponseDTO<bool>> Editar(CategoriaDTO modelo)
        {
            //PutAsJson("Controlador/Metodo")


            //Respuesta de haber ejecutado la API
            var response = await httpClient.PutAsJsonAsync("Categoria/Editar", modelo);


            //Respuesta 
            var result = await response.Content.ReadFromJsonAsync<ResponseDTO<bool>>();


            return result!;
        }

        public async Task<ResponseDTO<bool>> Eliminar(int id)
        {
            //DeleteFromJsonAsync("Controlador/Metodo")


            //Respuesta de haber ejecutado la API
            return await httpClient.DeleteFromJsonAsync<ResponseDTO<bool>>($"Categoria/Eliminar/{id}");
        }

        public async Task<ResponseDTO<List<CategoriaDTO>>> Lista(string buscar)
        {
            return await httpClient.GetFromJsonAsync<ResponseDTO<List<CategoriaDTO>>>($"Categoria/Lista/{buscar}");

        }

        public async Task<ResponseDTO<CategoriaDTO>> Obtener(int id)
        {
            return await httpClient.GetFromJsonAsync<ResponseDTO<CategoriaDTO>>($"Categoria/Obtener/{id}");

        }
    }
}

using Ecommerce.DTO;
using Ecommerce.WebAssembly.Servicios.Contrato;
using System.Net.Http.Json;
using System.Reflection;

namespace Ecommerce.WebAssembly.Servicios.Implementacion
{
    public class UsuarioServicio : IUsuarioServicio
    {
        private readonly HttpClient httpClient;

        public UsuarioServicio(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        //public UsuarioServicio(SignInManager<IdentityUser> signInManager)
        //{
                    
        //}

        public async Task<ResponseDTO<SesionDTO>> Autorizacion(LoginDTO modelo)
        {

            //PostAsJson("Controlador/Metodo")


            //Respuesta de haber ejecutado la API
            var response = await httpClient.PostAsJsonAsync("Usuario/Autorizacion", modelo);


            //Respuesta 
            var result = await response.Content.ReadFromJsonAsync<ResponseDTO<SesionDTO>>();


            return result!;
        }

        public async Task<ResponseDTO<UsuarioDTO>> Crear(UsuarioDTO modelo)
        {
            //PostAsJson("Controlador/Metodo")


            //Respuesta de haber ejecutado la API
            var response = await httpClient.PostAsJsonAsync("Usuario/Crear", modelo);


            //Respuesta 
            var result = await response.Content.ReadFromJsonAsync<ResponseDTO<UsuarioDTO>>();


            return result!;
        }

        public async Task<ResponseDTO<bool>> Editar(UsuarioDTO modelo)
        {
            //PutAsJson("Controlador/Metodo")


            //Respuesta de haber ejecutado la API
            var response = await httpClient.PutAsJsonAsync("Usuario/Editar", modelo);


            //Respuesta 
            var result = await response.Content.ReadFromJsonAsync<ResponseDTO<bool>>();


            return result!;
        }

        public async Task<ResponseDTO<bool>> Eliminar(int id)
        {
            //DeleteFromJsonAsync("Controlador/Metodo")


            //Respuesta de haber ejecutado la API
            return await httpClient.DeleteFromJsonAsync<ResponseDTO<bool>>($"Usuario/Eliminar/{id}");


        }

        public async Task<ResponseDTO<List<UsuarioDTO>>> Lista(string rol, string buscar)
        {
            return await httpClient.GetFromJsonAsync<ResponseDTO<List<UsuarioDTO>>>($"Usuario/Lista/{rol}/{buscar}");

        }

        public async Task<ResponseDTO<UsuarioDTO>> Obtener(int id)
        {
            return await httpClient.GetFromJsonAsync<ResponseDTO<UsuarioDTO>>($"Usuario/Obtener/{id}");

        }
    }
}

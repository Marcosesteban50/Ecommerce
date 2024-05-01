using Ecommerce.DTO;
using Ecommerce.WebAssembly.Servicios.Contrato;
using System.Net.Http.Json;

namespace Ecommerce.WebAssembly.Servicios.Implementacion
{


    

    public class VentaServicio : IVentaServicio
    {
        private readonly HttpClient httpClient;

        public VentaServicio(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<ResponseDTO<VentaDTO>> Registrar(VentaDTO modelo)
        {
            //PostAsJson("Controlador/Metodo")


            //Respuesta de haber ejecutado la API
            var response = await httpClient.PostAsJsonAsync("Venta/Registrar", modelo);


            //Respuesta 
            var result = await response.Content.ReadFromJsonAsync<ResponseDTO<VentaDTO>>();


            return result!;
        }
    }
}

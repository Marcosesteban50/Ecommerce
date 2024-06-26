﻿using Ecommerce.DTO;
using Ecommerce.WebAssembly.Servicios.Contrato;
using System.Net.Http.Json;

namespace Ecommerce.WebAssembly.Servicios.Implementacion
{
    public class DashboardServicio : IDashboardServicio
    {
        private readonly HttpClient httpClient;

        public DashboardServicio(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<ResponseDTO<DashboardDTO>> Resumen()
        {
            return await httpClient.GetFromJsonAsync<ResponseDTO<DashboardDTO>>($"Dashboard/Resumen");

        }
    }
}

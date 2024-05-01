using Blazored.LocalStorage;
using Blazored.Toast.Services;
using Ecommerce.DTO;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;




namespace Ecommerce.WebAssembly.Extensiones
{
    public class AutenticacionExtension : AuthenticationStateProvider
    {
        private readonly ILocalStorageService localStorageService;
        private readonly IToastService toastService;
        private ClaimsPrincipal sinInformacion = new ClaimsPrincipal(new ClaimsIdentity());

        public AutenticacionExtension(ILocalStorageService localStorageService,
            IToastService toastService)
        {
            this.localStorageService = localStorageService;
            this.toastService = toastService;
        }



        public async Task ActualizarEstadoAutenticacion(SesionDTO? sesionUsuario)
        {
            ClaimsPrincipal claimsPrincipal;
            


            if(sesionUsuario != null)
            {
                claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, sesionUsuario.IdUsuario.ToString()),
                    new Claim(ClaimTypes.Name, sesionUsuario.NombreCompleto!),
                    new Claim(ClaimTypes.Email, sesionUsuario.Correo!),
                    new Claim(ClaimTypes.Role, sesionUsuario.Rol!)
              //Tipo de Autenticacion JwtAuth
                },"JwtAuth"));



                //Guardando La informacion del usuario en el LSS = localstorageservice
                await localStorageService.SetItemAsync("sesionUsuario", sesionUsuario);
            }
            //
            else
            {
                //Asignandole al CP un  CP vacio
                claimsPrincipal = sinInformacion;
                //removiendo el item guardado en el LSS
                await localStorageService.RemoveItemAsync("sesionUsuario");


            }

            toastService.ShowSuccess("El item se removio satisfactoriamente");


            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }





        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            //obteniendo toda la info guardada en el LSS con llave de sesionUsuario
            var sesionUsuario = await localStorageService.GetItemAsync<SesionDTO>
                ("sesionUsuario");

            //si es Nulo le devolvemos informacion vacia y ya
            if(sesionUsuario is null)
            {
                return await Task.FromResult(new AuthenticationState(sinInformacion));


            }

            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, sesionUsuario.IdUsuario.ToString()),
                    new Claim(ClaimTypes.Name, sesionUsuario.NombreCompleto!),
                    new Claim(ClaimTypes.Email, sesionUsuario.Correo!),
                    new Claim(ClaimTypes.Role, sesionUsuario.Rol!)
              //Tipo de Autenticacion JwtAuth
                }, "JwtAuth"));

            return await Task.FromResult(new AuthenticationState(claimsPrincipal));


        }
    }
}

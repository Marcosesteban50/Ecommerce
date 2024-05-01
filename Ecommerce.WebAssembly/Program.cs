using Blazored.LocalStorage;
using Blazored.Toast;
using CurrieTechnologies.Razor.SweetAlert2;
using Ecommerce.WebAssembly;
using Ecommerce.WebAssembly.Extensiones;
using Ecommerce.WebAssembly.Servicios.Contrato;
using Ecommerce.WebAssembly.Servicios.Implementacion;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;




var builder = WebAssemblyHostBuilder.CreateDefault(args);




builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


                                                               //web Local nuestra
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7084/api/") });





builder.Services.AddBlazoredLocalStorage();


builder.Services.AddBlazoredToast();

//Scoped porque si sabemos que modelo trabajara por eso lleva algo dentro de <>



builder.Services.AddScoped<IUsuarioServicio, UsuarioServicio>();
builder.Services.AddScoped<ICategoriaServicio, CategoriaServicio>();
builder.Services.AddScoped<iProductoServicio, ProductoServicio>();
builder.Services.AddScoped<ICarritoServicio, CarritoServicio>();
builder.Services.AddScoped<IVentaServicio, VentaServicio>();
builder.Services.AddScoped<IDashboardServicio, DashboardServicio>();


builder.Services.AddSweetAlert2();


builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, AutenticacionExtension>();

await builder.Build().RunAsync();

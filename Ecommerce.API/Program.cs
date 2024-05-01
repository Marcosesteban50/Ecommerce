using Ecommerce.API.Controllers;
using Ecommerce.Repositorio.Contrato;
using Ecommerce.Repositorio.DBContext;
using Ecommerce.Repositorio.Implementacion;
using Ecommerce.Servicio.Contrato;
using Ecommerce.Servicio.Implementacion;
using Ecommerce.Utilidades;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;


var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//Conexion a la base de datos SqlServer
builder.Services.AddDbContext<DbecommerceContext>(opc => {

    opc.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));


});


builder.Services.AddHttpContextAccessor();




builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    
    
})
    .AddCookie()
    .AddGoogle(g =>
{
    g.ClientId = builder.Configuration["GoogleClientId"]!;
    g.ClientSecret = builder.Configuration["GoogleSecretId"]!;
    g.CallbackPath = "/signin-google";
});

builder.Services.AddMvc();


builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

//transient porque no especificamos para que por eso va <> vacio
builder.Services.AddTransient(typeof(IGenericoRepositorio<>), typeof(GenericoRepositorio<>));

//scoped porque si sabemos que modelo trabajara por eso lleva algo dentro de <>
builder.Services.AddScoped<IVentaRepositorio, VentaRepositorio>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));




//scoped porque si sabemos que modelo trabajara por eso lleva algo dentro de <>


builder.Services.AddScoped<IUsuarioServicio, UsuarioServicio>();
builder.Services.AddScoped<ICategoriaServicio, CategoriaServicio>();
builder.Services.AddScoped<IProductoServicio, ProductoServicio>();
builder.Services.AddScoped<IVentaServicio, VentaServicio>();
builder.Services.AddScoped<IDashboardServicio, DashboardServicio>();




builder.Services.AddCors(opc =>
{
    opc.AddPolicy("NuevaPolitica",app =>
    {
        app.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });


});




var app = builder.Build();
    
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseHttpsRedirection();

app.UseCors("NuevaPolitica");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

using AR_ECOMERCE.IoC;
using Autofac.Extensions.DependencyInjection;
using Autofac;
using AR.Common;
using AR.Common.InterfacesFirabase;
using Microsoft.AspNetCore.Http.Connections;
using AR.Core.Purchase.Common;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();
builder.Services.Configure<AppFirebase>(builder.Configuration.GetSection("AppFirebase"));
builder.Services.Configure<AppJWT>(builder.Configuration.GetSection("Jwt"));
builder.Services.AddHttpClient();
builder.Services.AddScoped<IFirebaseFN, FirebaseFN>();
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(bulider =>
{
    bulider.RegisterModule(new RepositoryAutofacModule(builder.Configuration.GetConnectionString("SqlConnection") ?? ""));
    bulider.RegisterModule<ApplicationsAutofacModule>();
    bulider.RegisterModule<DomainAutofacModule>();
    bulider.RegisterModule<FiltersAutofacModule>();
});
builder.Services.AddSignalR(c =>
{
    c.EnableDetailedErrors = true;
    c.ClientTimeoutInterval = TimeSpan.FromMinutes(1);
    c.KeepAliveInterval = TimeSpan.FromMinutes(1);
}).AddJsonProtocol();



var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(builder =>
{
    builder.WithOrigins("http://localhost:4200","http://localhost:4201","https://araccesorios.shop")
           .AllowAnyMethod()
           .AllowAnyHeader()
           .AllowCredentials()
           .WithExposedHeaders("Content-Disposition");
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapHub<ARHub>("/sockets/app");

app.MapControllers();

app.Run();

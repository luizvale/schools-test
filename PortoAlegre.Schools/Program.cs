using Microsoft.OpenApi.Models;
using PortoAlegre.BingMaps.Config;
using PortoAlegre.Schools.Config;
using PortoAlegre.Schools.Externals.Clients;
using PortoAlegre.Schools.Externals.Clients.Interfaces;
using PortoAlegre.Schools.Repository;
using PortoAlegre.Schools.Services;
using PortoAlegre.Schools.Services.Interfaces;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Azure.Extensions.AspNetCore.Configuration.Secrets;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<SchoolClientConfig>(
    builder.Configuration.GetSection(SchoolClientConfig.Client));

builder.Services.Configure<BingMapsClientConfig>(
    builder.Configuration.GetSection(BingMapsClientConfig.Client));

builder.Services.AddControllers();

builder.Services.AddHttpClient();


builder.Services.AddScoped<ISchoolsRepository, SchoolsRepository>();
builder.Services.AddScoped<ISchoolClient, SchoolClient>();
builder.Services.AddScoped<IRouteClient, RouteClient>();
builder.Services.AddScoped<IBingMapsClient, BingMapsClient>();
builder.Services.AddScoped<ISchoolService, SchoolService>();
builder.Services.AddScoped<ILocalSearchSercice, LocalSearchService>();

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";


builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      builder =>
                      {
                          builder.WithOrigins("http://localhost:3000")
                          .AllowAnyHeader();
                      });
});


builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",

        Title = "Cadastro Escolas",

        Description = "O cadastro das escolas traz informações relativas às escolas da rede própria e conveniada, " +
        "tais como: nome, endereço, telefone, bairro, região do OP a que pertence a escola, região do Conselho Tutelar, " +
        "situação de credenciamento, mantenedora da escola, dependência administrativa entre outros.",
    });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});


if (app.Environment.IsProduction())
{
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseCors(MyAllowSpecificOrigins);
app.MapControllers();
app.Run();

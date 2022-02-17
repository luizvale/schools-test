using Microsoft.OpenApi.Models;
using PortoAlegre.Schools.Config.infra;
using PortoAlegre.Schools.Config.Models;
using PortoAlegre.Schools.Externals.Clients;
using PortoAlegre.Schools.Externals.Clients.Interfaces;
using PortoAlegre.Schools.Repository;
using PortoAlegre.Schools.Services;
using PortoAlegre.Schools.Services.Interfaces;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

var keyVault = KeyVaultHelper.GetSecret();

builder.Services.Configure<BingMapsClientConfig>(config =>
{
    config.DistanceMatrixEndpoint = keyVault.DistanceMatrixEndpoint;
    config.RouteEndpoint = keyVault.RouteEndpoint;
    config.Key = keyVault.Key;
});

builder.Services.Configure<SchoolClientConfig>(
    builder.Configuration.GetSection(SchoolClientConfig.Client));

builder.Services.AddControllers();

builder.Services.AddHttpClient();


builder.Services.AddScoped<ISchoolsRepository, SchoolsRepository>();
builder.Services.AddScoped<ISchoolClient, SchoolClient>();
builder.Services.AddScoped<IRouteClient, RouteClient>();
builder.Services.AddScoped<IBingMapsClient, BingMapsClient>();
builder.Services.AddScoped<ISchoolService, SchoolService>();
builder.Services.AddScoped<ILocalSearchSercice, LocalSearchService>();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Cadastro Escolas",
        Description = "O cadastro das escolas traz informações relativas às escolas da rede própria e conveniada, tais como: nome, endereço, telefone, bairro, região do OP a que pertence a escola, " +
        "região do Conselho Tutelar, situação de credenciamento, mantenedora da escola, dependência administrativa entre outros.",
        TermsOfService = new Uri("http://datapoa.com.br")
    });

});

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

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint(url: "/swagger/v1/swagger.json", name: "Dados Abertos");
    options.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseCors(MyAllowSpecificOrigins);
app.MapControllers();
app.Run();

using Microsoft.OpenApi.Models;
using PortoAlegre.BingMaps.Config;
using PortoAlegre.Schools.Config;
using PortoAlegre.Schools.Externals.Clients;
using PortoAlegre.Schools.Externals.Clients.Interfaces;
using PortoAlegre.Schools.Repository;
using PortoAlegre.Schools.Services;
using PortoAlegre.Schools.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure<SchoolClientConfig>(
    builder.Configuration.GetSection(SchoolClientConfig.Client));

builder.Services.Configure<BingMapsClientConfig>(
    builder.Configuration.GetSection(BingMapsClientConfig.Client));

builder.Services.AddControllers();

builder.Services.AddHttpClient();


builder.Services.AddScoped<ISchoolsRepository, SchoolsRepository>();
builder.Services.AddScoped<ISchoolClient, SchoolClient>();
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

        TermsOfService = new Uri("http://www2.portoalegre.rs.gov.br/transparencia/"),
        Contact = new OpenApiContact
        {
            Name = "SIE - Sistema de Informações Educacionais",
            Url = new Uri("http://datapoa.com.br/")
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}
else
{

    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseCors(MyAllowSpecificOrigins);
app.MapControllers();
app.Run();

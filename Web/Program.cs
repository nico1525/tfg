using Blazorise;
using Blazorise.Bootstrap5;
using Blazorise.Charts;
using Blazorise.Icons.FontAwesome;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Web.Helpers;
using Web.Services;
using Web.Services.ConsumoServices;
using Web.Services.InformesServices;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddHttpClient();
builder.Services.AddScoped<IOrganizacionServices, OrganizacionServices>();
builder.Services.AddScoped<IUsuarioServices, UsuarioServices>();
builder.Services.AddScoped<IVehiculoServices, VehiculoServices>();
builder.Services.AddScoped<ITransporteServices, TransporteServices>();
builder.Services.AddScoped<IMaquinariaServices, MaquinariaServices>();
builder.Services.AddScoped<IInstalacionesFijasServices, InstalacionesFijasServices>();
builder.Services.AddScoped<IEmisionesFugitivasServices, EmisionesFugitivasServices>();
builder.Services.AddScoped<IElectricidadServices, ElectricidadServices>();

builder.Services.AddScoped<IOtrosConsumosServices, OtrosConsumosServices>();
builder.Services.AddScoped<IVehiculoConsumoServices, VehiculoConsumoServices>();
builder.Services.AddScoped<ITransporteConsumoServices, TransporteConsumoServices>();
builder.Services.AddScoped<IMaquinariaConsumoServices, MaquinariaConsumoServices>();
builder.Services.AddScoped<IInstalacionesFijasConsumoServices, InstalacionesFijasConsumoServices>();
builder.Services.AddScoped<IEmisionesFugitivasConsumoServices, EmisionesFugitivasConsumoServices>();
builder.Services.AddScoped<IElectricidadConsumoServices, ElectricidadConsumoServices>();

builder.Services.AddScoped<IInformeVehiculoService, InformeVehiculoService>();
builder.Services.AddScoped<IInformeTransporteService, InformeTransporteService>();
builder.Services.AddScoped<IInformeMaquinariaService, InformeMaquinariaService>();
builder.Services.AddScoped<IInformeEmisionesFugitivasService, InformeEmisionesFugitivasService>();
builder.Services.AddScoped<IInformeInstalacionesFijasService, InformeInstalacionesFijasService>();
builder.Services.AddScoped<IInformeElectricidadService, InformeElectricidadService>();
builder.Services.AddScoped<IInformeOtrosConsumosService, InformeOtrosConsumosService>();

builder.Services.AddScoped<ProtectedLocalStorage>();
builder.Services.AddSingleton<Storage>();
builder.Services.AddSweetAlert2();

builder.Services
    .AddBlazorise(options =>
    {
        options.Immediate = true;
    })
    .AddBootstrap5Providers();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();

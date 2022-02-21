using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ceramics_portfolio.Client;
using CeramicsPortfolio.Blazor.Models;
using Kentico.Kontent.Delivery.Abstractions;
using Kentico.Kontent.Delivery.Extensions;
using SendGrid.Extensions.DependencyInjection;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddDeliveryClient(builder.Configuration.Build());

builder.Services.AddSingleton<ITypeProvider, CustomTypeProvider>();

builder.Services.AddSendGrid(o =>
{
    o.ApiKey = "<YOUR_API_KEY>";
});

builder.Services.AddHttpClient();
;

await builder.Build().RunAsync();

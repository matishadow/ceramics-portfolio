using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ceramics_portfolio.Client;
using Contentful.Core;
using Contentful.Core.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using SendGrid.Extensions.DependencyInjection;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


const string httpClientName = "ContentfulClient";
var configuration = builder.Configuration.Build();
builder.Services.AddOptions();
builder.Services.Configure<ContentfulOptions>(configuration.GetSection("ContentfulOptions"));
builder.Services.TryAddSingleton<HttpClient>();
builder.Services.AddHttpClient(httpClientName);
builder.Services.TryAddTransient<IContentfulClient>((sp) =>
{
    var options = sp.GetService<IOptions<ContentfulOptions>>().Value;
    var factory = sp.GetService<IHttpClientFactory>();
    return new ContentfulClient(factory.CreateClient(httpClientName), options);
});


builder.Services.AddHttpClient();

await builder.Build().RunAsync();

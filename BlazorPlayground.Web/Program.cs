using BlazorPlayground.Web;
using BlazorPlayground.Web.Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

var configSection = builder.Configuration.GetSection("Server");
builder.Services.RegisterClient(configSection.Get<ApiClientConfiguration>());

var host = builder.Build();

await host.RunAsync();

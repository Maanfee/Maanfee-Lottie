using Maanfee.Lottie;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddMaanfeeLottie();

await builder.Build().RunAsync();

using Maanfee.Lottie;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddLottie();

await builder.Build().RunAsync();

using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Clight.LogoStudio.Wasm;
using Clight.Logo.Core.Services;
using Clight.Logo.Renderer.Services;
using Clight.Asset.Generator.Services;
using Clight.Brand.Guideline.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Register Clight Brand System Services via DI
builder.Services.AddSingleton<ILogoCalculator, LogoCalculator>();
builder.Services.AddSingleton<ISvgLogoRenderer, SvgLogoRenderer>();
builder.Services.AddSingleton<IAssetGenerator, AssetGenerator>();
builder.Services.AddSingleton<IGuidelineProvider, GuidelineProvider>();

await builder.Build().RunAsync();

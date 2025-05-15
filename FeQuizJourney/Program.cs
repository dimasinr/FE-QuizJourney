using Syncfusion.Blazor;
using FeQuizJourney.Components;
using FeQuizJourney.Components.Services;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient("ApiClient", client =>
{
    client.BaseAddress = new Uri("http://localhost:5121");
});

builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<RoomServices>();
builder.Services.AddScoped<QuestionServices>();
//builder.Services.AddBlazorBootstrap();
builder.Services.AddSyncfusionBlazor();
builder.Services.AddMudServices();

Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NNaF1cWWhMYVF0WmFZfVtgcV9GYVZURGYuP1ZhSXxWdkBiXH9bcHVQRmJdUUN9XUs=");


builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents(); 

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();

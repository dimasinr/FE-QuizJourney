using FeQuizJourney.Components;
using FeQuizJourney.Components.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient("ApiClient", client =>
{
    client.BaseAddress = new Uri("http://localhost:5121");
});

builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<RoomServices>();
builder.Services.AddScoped<QuestionServices>();

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

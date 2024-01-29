
using App.DALModels;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//builder.Services.AddAutoMapper(typeof(App.Mapping.PersonAutomapper));
//builder.Services.AddScoped<ICosmosDBService, CosmosDBService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Person}/{action=Index}/{id?}");

CosmosDBServiceProvider.Init().GetAwaiter().GetResult();

app.Run();

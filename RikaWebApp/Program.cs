
using Business.Services.Product;
using Business.Interfaces.OrderInterfaces;
using Business.Services.OrderServices;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IShoppingCartService, ShoppingCartService>();


// L�gg till tj�nster
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IProductServiceCategory, ProductServiceCategory>();
builder.Services.AddHttpClient(); // Registrera HttpClient



builder.Services.AddHttpClient("AzureFunctionClient", client =>
{
    client.BaseAddress = new Uri("https://productprovidergraphql.azurewebsites.net/api/"); 
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

builder.Services.AddScoped<ProductService>();


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

//om vi vill att hantera role

//using (var scope = app.Services.CreateScope())
//{
//    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
//    string[] roles = ["Admin", "superAdmin", "Manager", "User"];
//    foreach (string role in roles)
//    {
//        if (!await roleManager.RoleExistsAsync(role))
//        {
//            await roleManager.CreateAsync(new IdentityRole(role));
//        }

//    }

//}




app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();



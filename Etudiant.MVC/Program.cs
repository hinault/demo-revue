using Etudiants.MVC.Interfaces;
using Etudiants.MVC.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Etudiants.MVC.Data;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("EtudiantsMVCContextConnection");;

builder.Services.AddDbContext<EtudiantsMVCContext>(options =>
    options.UseSqlite(connectionString));;

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<EtudiantsMVCContext>();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequiredLength = 6;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
    
});

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDataProtection();

builder.Services.AddHttpClient<IEtudiantsService, EtudiantsServiceProxy>(client => 
                        client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("UrlEtudiantsAPI")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();
app.Run();

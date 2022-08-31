using LeaveManagenet.Web.Configurations;
using LeaveManagenet.Web.Contracts;
using LeaveManagenet.Web.Data;
using LeaveManagenet.Web.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<Employee>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()/*esta permite q reconosca los roles*/
    .AddEntityFrameworkStores<ApplicationDbContext>();
// recordar q en Views/Shared/_LoginPartial esta xdefault "SignInManager<IdentityUser>" y aca
// cambiamos a <Employee>, asi q hay q cambiar alla tb

// cuando inyecte un ILeaveTypeRepository va a ocupar LeaveTypeRepository
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<ILeaveTypeRepository, LeaveTypeRepository>();

builder.Services.AddAutoMapper(typeof(MapperConfig));

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();

/*
 * Las areas son las distintas partes dentro de la aplicacion
 * xejemplo, la admin-section y user-section, y se les puede poner diferentes layouts
 * o comportamientos
 */

/*
 *
 */
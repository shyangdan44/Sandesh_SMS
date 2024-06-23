using Microsoft.EntityFrameworkCore;
using Sandesh_SMS.Data;
using Sandesh_SMS.Repositories;
using Sandesh_SMS.ViewModels;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();


//Register DbContext
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddDefaultTokenProviders().AddDefaultUI().AddEntityFrameworkStores<AppDbContext>();

//builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddDefaultTokenProviders()
//    .AddEntityFrameworkStores<AppDbContext>();
//Register Class service
builder.Services.AddScoped<IClassRepository, ClassRepository>();
//Register Course service
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
//Register Student service
builder.Services.AddScoped<IStudentRepository, StudentRepository>();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
using (var scope = app.Services.CreateScope())
{
    await DbSeeder.SeedRolesAndAdminAsync(scope.ServiceProvider);
}
app.Run();

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SThakarAssignment2.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<CourseManagerDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("CourseManager")));
builder.Services.AddSingleton<CookieServices>();
builder.Services.AddTransient<EmailServices>();
builder.Services.AddHttpContextAccessor();
// Add services to the container.
builder.Services.AddControllersWithViews();


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

app.Run();

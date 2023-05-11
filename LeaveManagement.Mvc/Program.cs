using LeaveManagement.Mvc.Contracts;
using LeaveManagement.Mvc.Services.Base;
using LeaveManagement.Mvc.Services;
using System.Reflection;
using LeaveManagement.Mvc.Middleware;
using Microsoft.AspNetCore.Authentication.Cookies;
using Serilog.Events;
using Serilog;

//Log.Logger = new LoggerConfiguration()
//    .WriteTo.File(
//        path: "C:\\Users\\SamRayane\\Documents\\Visual Studio 2022\\visual stadio\\Logs\\asp.net core\\hotelListing\\LeaveMlog-.txt",
//        outputTemplate: "{TimeStamp: yyyy-MM-dd HH:mm:ss.fff zzz}[{Level:u3}] {Message:lg}{NewLine}{Exception}",
//        rollingInterval: RollingInterval.Day,
//        restrictedToMinimumLevel: LogEventLevel.Information
//    ).CreateLogger();
try
{
    Log.Information("Application Is Starting");

    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    builder.Host.UseSerilog();
    builder.Services.AddHttpContextAccessor();


    builder.Services.Configure<CookiePolicyOptions>(options =>
    {
        options.MinimumSameSitePolicy = SameSiteMode.None;
    });
    builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
    {
        options.LoginPath = new PathString("/users/login");
    }); ;
    builder.Services.AddControllersWithViews();

    builder.Services.AddHttpClient<IClient, Client>(cl => cl.BaseAddress = new Uri("https://localhost:7132"));
    builder.Services.AddTransient<IAuthenticationService, AuthenticationService>();
    builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());


    builder.Services.AddScoped<ILeaveTypeService, LeaveTypeService>();
    builder.Services.AddScoped<ILeaveAllocationService, LeaveAllocationService>();
    builder.Services.AddScoped<ILeaveRequestService, LeaveRequestService>();
    builder.Services.AddSingleton<ILocalStorageService, LocalStorageService>();
    var app = builder.Build();
    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseCookiePolicy();
    app.UseAuthentication();

    app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.UseMiddleware<RequestMiddleware>();

    app.UseRouting();

    app.UseAuthorization();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");


    app.Run();

}


catch (Exception e)
{
    Log.Fatal(e, "Application Failed to start");
}
finally
{
    Log.CloseAndFlush();
}

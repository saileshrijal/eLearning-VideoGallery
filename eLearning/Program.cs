
using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Extensions;
using eLearning.Data;
using eLearning.Repository;
using eLearning.Repository.Interface;
using eLearning.Service;
using eLearning.Service.Interface;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    Console.WriteLine("Connection String: " + connectionString);
    builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

    builder.Services.AddControllersWithViews();
    // Repositories
    builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
    builder.Services.AddScoped<IGradeRepository, GradeRepository>();
    builder.Services.AddScoped<ISubjectRepository, SubjectRepository>();

    // Services
    builder.Services.AddScoped<IGradeService, GradeService>();
    builder.Services.AddScoped<ISubjectService, SubjectService>();


    builder.Services.AddNotyf(config => { config.DurationInSeconds = 10; config.IsDismissable = true; config.Position = NotyfPosition.BottomRight; });

}

var app = builder.Build();

{
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        app.UseHsts();
    }
    app.UseNotyf();
    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthorization();

    app.MapControllerRoute(
     name: "area",
     pattern: "{area=Admin}/{controller=Home}/{action=Index}/{id?}");

    app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

    app.Run();
}

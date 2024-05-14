using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProniaMVCProject.Business.Services.Abstracts;
using ProniaMVCProject.Business.Services.Concretes;
using ProniaMVCProject.Core.Models;
using ProniaMVCProject.Core.RepositoryAbstracts;
using ProniaMVCProject.Data.DAL;
using ProniaMVCProject.Data.RepositoryConcretes;

namespace ProniaMVCProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();
            

            // Add services to the container.
            builder.Services.AddDbContext<AppDbContext>(options=>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("default"));
            });

            builder.Services.AddIdentity<AppUser, IdentityRole>(opt =>
            {
                opt.Password.RequireNonAlphanumeric = true;
                opt.Password.RequireLowercase = true;
                opt.Password.RequireUppercase = true;
                opt.Password.RequireDigit = true;
                opt.Password.RequiredLength = 8;

                opt.User.RequireUniqueEmail = false;


            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<IFeatureService, FeatureService>();
            builder.Services.AddScoped<ITagService, TagService>();

            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<IFeatureRepository, FeatureRepository>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<ITagRepository, TagRepository>();

            builder.Services.AddScoped<ISliderRepository, SliderRepository>();
            builder.Services.AddScoped<ISliderService, SliderService>();




            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "areas",
                pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
          );

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}

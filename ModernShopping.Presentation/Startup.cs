using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ModernShopping.Application.Contracts;
using ModernShopping.Application.Services;
using ModernShopping.Persistence;
using ModernShopping.Presentation.ModelBinders;
using Swashbuckle.AspNetCore.Swagger;

namespace ModernShopping.Presentation
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<NorthwindContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("NorthwindDatabase")));
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<ICartService, CartService>();

            services.AddMvc(options =>
            {
                options.ReturnHttpNotAcceptable = true;
                options.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());

                options.ModelBinderProviders.Insert(0, new CustomArrayModelBinderProvider());
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1beta", new Info { Title = "Modern shopping api", Version = "v1beta" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                // Webpack initialization with hot-reload.
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true,
                });
            }
            else
            {
                app.UseExceptionHandler(appBuilder =>
                {
                    appBuilder.Run(async context =>
                    {
                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync("Unexpected error. Please contact administrator!");
                    });
                });
            }

            app.UseStaticFiles();

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1beta/swagger.json", "Modern shopping api");
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
            });

            
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ModernShopping.Application.Contracts;
using ModernShopping.Application.Services;
using ModernShopping.Persistence;
using Swashbuckle.AspNetCore.Swagger;

namespace ModernShopping.WebUI
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

			services.AddMvc(options =>
			{
			    options.ReturnHttpNotAcceptable = true;
                options.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());

			}).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

		    services.AddSwaggerGen(options =>
		    {
		        options.SwaggerDoc("v1beta", new Info {Title = "Modern shopping api", Version = "v1beta"});
		    });
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
		    if (env.IsDevelopment())
		    {
		        app.UseDeveloperExceptionPage();
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

		    app.UseSwagger();
		    app.UseSwaggerUI(options =>
		    {
		        options.SwaggerEndpoint("/swagger/v1beta/swagger.json", "Modern shopping api");
		    });

			app.UseMvc();
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Client.WebAPI.Services;
using Client.WebAPI.Helper;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using Client.WebAPI.Entities;
using AutoMapper;
using Client.WebAPI.Mappings;
using Client.WebAPI.Repositories;
using Microsoft.OpenApi.Models;
namespace Client.WebAPI
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
			services.AddSingleton<IConfiguration>(Configuration);
			ConfigureCors(services);
			ConfigureContext(services);
			services.AddScoped<DbContext, ClientDBContext>();
			ConfigureMapper(services);
			ConfigureContainer(services);
			ConfigureAuthentication(services);
			services.AddControllers();
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "Client Managment System", Version = "v1" });
				//c.AddSecurityDefinition("Authorizaion", new OpenApiSecurityScheme
				//{
				//	Description = "Api key needed to access the endpoints.",
				//	In = ParameterLocation.Header,
				//	Name = "Authorizaion",
				//	Type = SecuritySchemeType.ApiKey
				//});
				//c.AddSecurityRequirement(new OpenApiSecurityRequirement
				//{
				//	{
				//		new OpenApiSecurityScheme
				//		{
				//			Name = "Authorizaion",
				//			Type = SecuritySchemeType.ApiKey,
				//			In = ParameterLocation.Header,
				//			Reference = new OpenApiReference
				//			{
				//				Type = ReferenceType.SecurityScheme,
				//				Id = "Authorizaion"
				//			},
				//		 },
				//		 new string[] {}
				//	}
				//});
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseRouting();
			app.UseCors("AllowAllOrigins");
			app.UseSwagger();
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "Client Management");

			});

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}

		#region Helper Methods
		private void ConfigureCors(IServiceCollection services)
		{
			services.AddCors(options =>
			{
				options.AddPolicy("AllowAllOrigins",
					builder =>
					{
						builder.AllowAnyMethod()
						.AllowAnyHeader()
						.AllowAnyOrigin();
					});
			});
		}
		private void ConfigureContext(IServiceCollection services)
		{
			services.AddDbContext<ClientDBContext>
				(options =>
					options.UseSqlServer(Configuration.GetConnectionString("DataContext"))
				);
		}
		private void ConfigureContainer(IServiceCollection services)
		{
			services.AddScoped<IUserService, UserService>();
			services.AddScoped<IUserRepository, UserRepository>();
		}
		private void ConfigureMapper(IServiceCollection services)
		{
			var mappingConfig = new MapperConfiguration(mc =>
			{
				mc.AddProfile(new ClientMap());
				mc.AddProfile(new UserMap());

			});
			IMapper mapper = mappingConfig.CreateMapper();
			services.AddSingleton(mapper);
		}
		private void ConfigureAuthentication(IServiceCollection services)
		{
			var appSettingsSection = Configuration.GetSection("AppSettings");
			services.Configure<AppSettings>(appSettingsSection);

			var appSettings = appSettingsSection.Get<AppSettings>();
			var key = Encoding.ASCII.GetBytes(appSettings.Secret);
			services.AddAuthentication(x =>
			{
				x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
		   .AddJwtBearer(x =>
		   {
			   x.RequireHttpsMetadata = false;
			   x.SaveToken = true;
			   x.TokenValidationParameters = new TokenValidationParameters
			   {
				   ValidateIssuerSigningKey = true,
				   IssuerSigningKey = new SymmetricSecurityKey(key),
				   ValidateIssuer = false,
				   ValidateAudience = false
			   };
		   });
		}
		#endregion
	}
}

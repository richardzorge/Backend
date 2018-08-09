using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Backend.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Npgsql;

namespace Backend
{
    public class Startup
    {
        NLog.Logger Logger
        {
            get
            {
                return NLog.LogManager.GetCurrentClassLogger();
            }
        }

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Logger.Trace("Startup IN");
            Configuration = configuration;
            Logger.Trace("Startup OUT");
        }

        public void ConfigureServices(IServiceCollection services)
        {
            Logger.Trace("ConfigureServices IN");

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = Convert.ToBoolean(Configuration["Jwt:ValidateIssuer"]),
                    ValidateAudience = Convert.ToBoolean(Configuration["Jwt:ValidateAudience"]),
                    ValidateLifetime = Convert.ToBoolean(Configuration["Jwt:ValidateLifetime"]),
                    ValidateIssuerSigningKey = Convert.ToBoolean(Configuration["Jwt:ValidateIssuerSigningKey"]),
                    ValidIssuer = Configuration["Jwt:ValidIssuer"],
                    ValidAudience = Configuration["Jwt:ValidAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                };
            });

            Configuration["DatabaseConnectionString"]=string.Format("Host={0};Port={1};Database={2};Username={3};Password={4}", Configuration["Database:Host"], Configuration["Database:Port"], Configuration["Database:Database"], Configuration["Database:User"], Configuration["Database:Password"]);
            services.AddSingleton<IConfiguration>(Configuration);

            services.AddDbContext<cap01devContext>(options => { options.UseNpgsql(Configuration["DatabaseConnectionString"]); });

            services.AddMvc();
            Logger.Trace("ConfigureServices OUT");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            Logger.Trace("Configure IN");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
        
            app.UseAuthentication();
            app.UseMvc();
            Logger.Trace("Configure OUT");
        }
    }
}

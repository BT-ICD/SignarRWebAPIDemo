using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SignarRWebAPIDemo.AppRepository;
using SignarRWebAPIDemo.AuthData;
using SignarRWebAPIDemo.DataContext;
using SignarRWebAPIDemo.HubFilter;
using SignarRWebAPIDemo.MyHub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignarRWebAPIDemo
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

            services.AddControllers();
            //SignalR - required CORS with Origin specification.  http://localhost:4200
            services.AddCors(options=>
            {
                options.AddPolicy("MYCORSPOLICY",
                    builder=>
                    {
                        builder.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod().AllowCredentials();
                    });
            });
            

            services.AddDbContext<SignalRLearningDBContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("cnn"))
            );
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<SignalRLearningDBContext>()
                .AddDefaultTokenProviders();
            //Authenticate Repository and Token options as dependency injection
            services.AddScoped<IAuthenticate, AuthenticateRepository>();
            services.AddScoped<IAppUser, AppUserRepository>();

            services.Configure<TokenSettingsOptions>(Configuration.GetSection(TokenSettingsOptions.TokenSettings));
            services.AddScoped<TokenGenerator>();

            //Authentication
            services.AddAuthentication(
                options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

                }
                ).AddJwtBearer(
                options =>
                {
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = false;
                    //options.Audience = "GrowthApp";
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = "SignalRWebAPI",
                        ValidAudience = "SignalRWebAPI",
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("aaaaaaaaaaaaaaaa"))
                    };
                    //Authentication token for hub
                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            var accessToken = context.Request.Query["access_token"];

                            // If the request is for our hub...
                            var path = context.HttpContext.Request.Path;
                            if (!string.IsNullOrEmpty(accessToken) &&
                                (path.StartsWithSegments("/messagehub")))
                            {
                                // Read the token out of the query string
                                context.Token = accessToken;
                            }
                            return Task.CompletedTask;
                        }
                    };
                });
            //To add SignalR - This should be added after AddCors 
            services.AddSignalR(options =>
            {
                options.AddFilter<CustomHubFilter>();
            });
            //    .AddHubOptions<MessageHub>(options=> {
            //    options.AddFilter(new CustomHubFilter());
            //});
            services.AddSingleton<IUserIdProvider, EmailBasedUserIdProvider>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SignarRWebAPIDemo", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SignarRWebAPIDemo v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors("MYCORSPOLICY");
            //Authentication
            app.UseAuthentication();

            app.UseAuthorization();
            //app.UseMiddleware<CustomMiddleware>();
            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapControllers();
                endpoints.MapControllers().RequireCors("MYCORSPOLICY");
                
                endpoints.MapHub<MessageHub>("/messagehub");

            });
        }
    }
}

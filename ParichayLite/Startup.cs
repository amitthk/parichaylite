using ParichayLite.Domain.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json.Serialization;
using ParichayLite.Data;
using ParichayLite.Domain;
using ParichayLite.Domain.Util;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Framework.ConfigurationModel;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.Data.Entity;
using AspNet.Security.OpenIdConnect.Server;
using Newtonsoft.Json.Schema;
using Microsoft.AspNet.Authentication.Google;
using Microsoft.AspNet.Authentication.OAuth;
using System.Threading.Tasks;
using Microsoft.Extensions.WebEncoders;
using Microsoft.AspNet.Http;
using ParichayLite.Models.Providers;
using ParichayLite.Models.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Extensions.Configuration;

namespace ParichayLite
{
    public class Startup
    {
        private Microsoft.Extensions.Configuration.IConfiguration config;

        public Startup(IHostingEnvironment env, Microsoft.Extensions.PlatformAbstractions.IApplicationEnvironment appEnv)
        {
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(appEnv.ApplicationBasePath)
                .AddEnvironmentVariables()
            .AddJsonFile("config.json")
            .AddJsonFile("config.dev.json", true);

            config = configurationBuilder.Build();

            //var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            //jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            ConfigureDI(services);

            services.AddSwaggerGen();
            services.ConfigureSwaggerGen(opt => {
                opt.SingleApiVersion(new Swashbuckle.SwaggerGen.Generator.Info
                {
                    Version = "v1",
                    Title = "ParichayLite API",
                    Description = "ParichayLite API",
                    TermsOfService = "None"
                });
            });

           
        }

        private void ConfigureDI(IServiceCollection services)
        {
            string identityConnectionString = config["Data:Identity:ConnectionString"];


            services.AddScoped<IdentityDataContext>();

            services.AddEntityFramework().AddSqlite().AddDbContext<IdentityDataContext>(cfg => cfg.UseSqlite(identityConnectionString));

            services.AddIdentity<Models.Identity.ApplicationUser, IdentityRole>().AddEntityFrameworkStores<Models.Identity.IdentityDataContext>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseMvc(routes => routes.MapRoute(
    "Default", "{controller=Home}/{action=Index}/{id?}"));
            // BundleConfig.RegisterBundles();


            app.UseIdentity();


            //ConfigureOAuth(app, env);


            if (config.Get<bool>("debug"))
            {
                app.UseDeveloperExceptionPage();
                app.UseRuntimeInfoPage();
                app.UseStatusCodePages();
            }
            else
            {
                app.UseExceptionHandler("/home/error");
            }

            app.UseStaticFiles();      
            app.UseSwaggerGen();
            app.UseSwaggerUi();

            loggerFactory.MinimumLevel = LogLevel.Information;
            loggerFactory.AddConsole();
            loggerFactory.AddDebug();

            //InitializeData(container);

        }

    void ConfigureOAuth(IApplicationBuilder app, IHostingEnvironment env)
    {

            app.UseCookieAuthentication(options =>
            {
                options.LoginPath = new PathString("/account/login");
                options.AutomaticAuthenticate = true;
                options.AuthenticationScheme = "Cookies";
            });

            app.UseGoogleAuthentication(options =>
            {
                options.ClientId = "560027070069-37ldt4kfuohhu3m495hk2j4pjp92d382.apps.googleusercontent.com";
                options.ClientSecret = "n2Q-GEw9RQjzcRbU3qhfTj8f";
                options.Events = new OAuthEvents()
                {
                    OnRemoteError = ctx =>

                    {
                        ctx.Response.Redirect("/error?ErrorMessage=" + UrlEncoder.Default.UrlEncode(ctx.Error.Message));
                        ctx.HandleResponse();
                        return Task.FromResult(0);
                    }
                };

            });

            app.UseOpenIdConnectServer(options => {
                options.Provider = new AuthorizationProvider();
            });
        }


        private void InitializeData(IApplicationBuilder app)
        {
            //var dbctx= app.ApplicationServices.GetRequiredService<SqliteContext>();

            //if (dbctx.Clients.Count == 0)
            //{
            //    dbctx.Clients.Add(new Client
            //    {
            //        Id = "ngAuthApp",
            //        Secret = Helper.GetHash("abc@123"),
            //        Name = "AngularJS front-end Application",
            //        ApplicationType = Domain.Models.ApplicationTypes.JavaScript,
            //        Active = true,
            //        RefreshTokenLifeTime = 7200,
            //        AllowedOrigin = "http://localhost:61528",
            //        //AllowedOrigin = "http://ngauthenticationweb.azurewebsites.net"
            //    });

            //    dbctx.Clients.Add(new Client
            //    {
            //        Id = "consoleApp",
            //        Secret = Helper.GetHash("123@abc"),
            //        Name = "Console Application",
            //        ApplicationType = Domain.Models.ApplicationTypes.NativeConfidential,
            //        Active = true,
            //        RefreshTokenLifeTime = 14400,
            //        AllowedOrigin = "*"
            //    });
            //    dbctx.Database.SaveChanges();
            //}
        }
    }
}
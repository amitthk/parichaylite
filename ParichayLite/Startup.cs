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

namespace ParichayLite
{
    public class Startup
    {
        private IConfiguration config;

        public Startup()
        {
            config = new Configuration()
                .AddEnvironmentVariables()
            .AddJsonFile("config.json")
            .AddJsonFile("config.dev.json", true);


            //var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            //jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();


            ConfigureDependencies(services);
            

            services.AddSwaggerGen();
            services.ConfigureSwaggerGen(opt => {
                opt.SingleApiVersion(new Swashbuckle.SwaggerGen.Generator.Info
                {
                    Version = "v1",
                    Title = "MIMS HL7 API",
                    Description = "MIMS HL7 parser",
                    TermsOfService = "None"
                });
            });

           
        }

        private void ConfigureDependencies(IServiceCollection services)
        {
            services.AddSingleton<DbContext>();

            services.AddSingleton<AuthRepository>();
            services.AddSingleton<ProjectsRepository>();


            services.AddSingleton<ParichayLite.Domain.ApplicationIdentityContext>();

            services.AddSingleton<UserManager<User>>();

            services.AddSingleton<RoleManager<Role>>();
         
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.MinimumLevel = LogLevel.Information;
            loggerFactory.AddConsole();
            loggerFactory.AddDebug();
            app.UseMvc(routes => routes.MapRoute(
    "Default", "{controller=Home}/{action=Index}/{id?}"));
            // BundleConfig.RegisterBundles();




            ///*******
            ////builder.RegisterType<SimpleAuthorizationServerProvider>()
            ////    .AsImplementedInterfaces<IOAuthAuthorizationServerProvider, ConcreteReflectionActivatorData>()
            ////    .WithParameters(new Parameter[]
            ////    {
            ////        new NamedParameter("publicClientId", "self"),
            ////        new ResolvedParameter((info, context) => info.Name == "userManager",
            ////            (info, context) => context.Resolve<ApplicationUserManager>())
            ////    });

            // ********/




           //// app.Use(typeof(AuthMiddleware)); This is dummy middleware no longer used.

            //var fileSystem = new PhysicalFileSystem(@".\");
            //var options = new FileServerOptions {
            //          EnableDirectoryBrowsing = false,
            //          FileSystem = fileSystem
            //          };

            ConfigureOAuth(app, env);


            string isdebug = "";
            if (config.TryGet("debug", out isdebug) && (isdebug.ToLower().Equals("true")))
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

            //InitializeData(container);

        }

        private void ConfigureOAuth(IApplicationBuilder app, IHostingEnvironment env)
        {

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


            //    app.UseOpenIdConnectServer(options => {
            //        options.Provider = new OpenIdConnectServerProvider
            //        {
            //            // Implement OnValidateAuthorizationRequest to support interactive flows (code/implicit/hybrid).
            //            OnValidateAuthorizationRequest = context => {
            //                // Note: you MUST NOT validate the request if client_id is invalid or if redirect_uri
            //                // doesn't correspond to a trusted URL associated with the client application.
            //                // You SHOULD also strongly consider validating the type of the client application
            //                // (public or confidential) to prevent code flow -> implicit flow downgrade attacks.
            //                if (string.Equals(context.ClientId, "client_id", StringComparison.Ordinal) &&
            //                    string.Equals(context.RedirectUri, "redirect_uri", StringComparison.Ordinal))
            //                {
            //                    context.Validate();
            //                }

            //                // Note: if Validate() is not explicitly called,
            //                // the request is automatically rejected.
            //                return Task.FromResult(0);
            //            }

            //// Implement OnValidateTokenRequest to support flows using the token endpoint.
            //            OnValidateTokenRequest = context => {
            //    // Note: you can skip the request validation when the client_id
            //    // parameter is missing to support unauthenticated token requests.
            //    // if (string.IsNullOrEmpty(context.ClientId)) {
            //    //     context.Skip();
            //    // }

            //    // Note: to mitigate brute force attacks, you SHOULD strongly consider applying
            //    // a key derivation function like PBKDF2 to slow down the secret validation process.
            //    // You SHOULD also consider using a time-constant comparer to prevent timing attacks.
            //    if (string.Equals(context.ClientId, "client_id", StringComparison.Ordinal) &&
            //        string.Equals(context.ClientSecret, "client_secret", StringComparison.Ordinal))
            //    {
            //        context.Validate();
            //    }

            //    // Note: if Validate() is not explicitly called,
            //    // the request is automatically rejected.
            //    return Task.FromResult(0);
            //}
            //        };
            //    });
        }

        private void InitializeData(IApplicationBuilder app)
        {
            var dbctx= app.ApplicationServices.GetRequiredService<SqliteContext>();

            if (dbctx.Clients.Count == 0)
            {
                dbctx.Clients.Add(new Client
                {
                    Id = "ngAuthApp",
                    Secret = Helper.GetHash("abc@123"),
                    Name = "AngularJS front-end Application",
                    ApplicationType = Domain.Models.ApplicationTypes.JavaScript,
                    Active = true,
                    RefreshTokenLifeTime = 7200,
                    AllowedOrigin = "http://localhost:61528",
                    //AllowedOrigin = "http://ngauthenticationweb.azurewebsites.net"
                });

                dbctx.Clients.Add(new Client
                {
                    Id = "consoleApp",
                    Secret = Helper.GetHash("123@abc"),
                    Name = "Console Application",
                    ApplicationType = Domain.Models.ApplicationTypes.NativeConfidential,
                    Active = true,
                    RefreshTokenLifeTime = 14400,
                    AllowedOrigin = "*"
                });
                dbctx.Database.SaveChanges();
            }
        }
    }
}
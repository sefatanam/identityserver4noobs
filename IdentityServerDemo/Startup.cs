using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IdentityServerDemo
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices (IServiceCollection services)
        {
            //Here will be using all the Static Resources, Clients, and Users we had defined in our IdentityConfiguration class.
            services.AddIdentityServer()
                     .AddInMemoryClients(IdentityConfiguration.Clients)
                     .AddInMemoryIdentityResources(IdentityConfiguration.IdentityResources)
                     .AddInMemoryApiResources(IdentityConfiguration.ApiResources)
                     .AddInMemoryApiScopes(IdentityConfiguration.ApiScopes)
                     .AddTestUsers(IdentityConfiguration.TestUsers)
                     .AddDeveloperSigningCredential();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            // For Initialize Identity Server 4
            app.UseIdentityServer();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }

        // N/B :
        // Basically, IdentityServer needs certificates to verify it’s usage.
        // But again, for development purposes and since we do not have any certificate with us, we use the AddDeveloperSigningCredential() extension

        /* For see identity server credentials
         * Go there after run this project 
         * https://localhost:44368/.well-known/openid-configuration
         */
    }
}

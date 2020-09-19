using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Couchbase.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AppUnderTest
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
            services.AddRazorPages();

            services.AddCouchbase(options => Configuration.GetSection("Couchbase").Bind(options));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // We're running in dev/test if Couchbase:ReadyFile is set, so we need to wait for
            // Couchbase to be initialized before moving on with application startup. Wait for the
            // file to be present.
            var couchbaseReadyFile = Configuration.GetValue("Couchbase:ReadyFile", "");
            if (!string.IsNullOrEmpty(couchbaseReadyFile))
            {
                var logger = app.ApplicationServices.GetRequiredService<ILogger<Startup>>();
                while (!File.Exists(couchbaseReadyFile))
                {
                    logger.LogWarning("Waiting for Couchbase startup...");
                    Thread.Sleep(1000);
                }
            }
        }
    }
}

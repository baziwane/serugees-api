using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Debug;
using Microsoft.AspNetCore.Mvc.Formatters;
using Newtonsoft.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Serugees.Api.Services;
using Serugees.Api.Models;

namespace Serugees.Api
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .AddMvcOptions(o => o.OutputFormatters.Add(
                    new XmlDataContractSerializerOutputFormatter()
                ))
                .AddJsonOptions(o => {
                    if(o.SerializerSettings.ContractResolver!=null)
                    {
                        var castedResolver = o.SerializerSettings.ContractResolver
                            as DefaultContractResolver;
                        castedResolver.NamingStrategy = null;
                    }
                });
            // inject our localMailService to allow sending mail whenever a new payment is registered.    
            services.AddTransient<IMailService, LocalMailService>();
            //var connectionString = Configuration.GetValue<string>("PostgresDb:ConnectionString") ?? Configuration.GetConnectionString("DefaultConnection_docker");
            //services.AddDbContext<SerugeesContext>(options => options.UseNpgsql(connectionString));
            services.AddDbContext<SerugeesContext>(options => options.UseInMemoryDatabase());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                loggerFactory.AddConsole();
                loggerFactory.AddDebug();
            }
            else{
                app.UseExceptionHandler();
            }
            app.UseStatusCodePages();
            app.UseMvc();
        
            // app.Run(async (context) =>
            // {
            //     await context.Response.WriteAsync("Hello World!");
            // });
        }
    }
}

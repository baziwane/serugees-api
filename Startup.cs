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
using Microsoft.Extensions.Configuration.Json;
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
        public IConfigurationRoot Configuration { get; }
         public Startup(IHostingEnvironment env)
        {
             var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
                
            Configuration = builder.Build();
        }

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
            //var connectionString = Configuration.GetValue<string>("PostgresDb:ConnectionString") ?? Configuration.GetConnectionString("DockerConnection");
            var connectionString = Configuration.GetConnectionString("SerugeesDbConnectionString");
            services.AddDbContext<SerugeesContext>(options => options.UseNpgsql(connectionString));
            //services.AddDbContext<SerugeesContext>(options => options.UseInMemoryDatabase());
            services.AddScoped<ISerugeesRepository, SerugeesRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory,
            SerugeesContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                loggerFactory.AddConsole();
                loggerFactory.AddDebug();
            }
            else
            {
                app.UseExceptionHandler();
            }

            context.EnsureSeedDataForContext();
            app.UseStatusCodePages();
            AutoMapper.Mapper.Initialize(cfg => 
            {
                cfg.CreateMap<Entities.Member, Models.MemberWithoutLoansDto>();
                cfg.CreateMap<Entities.Member, Models.MemberDto>();
                cfg.CreateMap<Entities.Loan, Models.LoanDto>();
                cfg.CreateMap<Entities.Loan, Models.LoanWithoutPaymentsDto>();
                cfg.CreateMap<Models.CreateLoanDto, Entities.Loan>();
                cfg.CreateMap<Models.CreateMemberDto, Entities.Member>();
                cfg.CreateMap<Entities.Payment, Models.PaymentDto>();
                cfg.CreateMap<Models.CreatePaymentDto, Entities.Payment>();
            });
            app.UseMvc();
        }
    }
}

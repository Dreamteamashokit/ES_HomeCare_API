using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ES_HomeCare_API.WebAPI.Data;
using ES_HomeCare_API.WebAPI.Data.IData;
using ES_HomeCare_API.WebAPI.Service;
using ES_HomeCare_API.WebAPI.Service.IService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using WebAPI_SAMPLE.WebAPI.Data;
using WebAPI_SAMPLE.WebAPI.Data.IData;
using WebAPI_SAMPLE.WebAPI.Service;
using WebAPI_SAMPLE.WebAPI.Service.IService;

namespace WebAPI_SAMPLE
{
    public class Startup
    {
        private readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IAccountData, AccountData>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<ILoginData, LoginData>();
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<IClientData, ClientData>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IEmployeeData, EmployeeData>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<ICustomerData, CustomerData>();
            services.AddScoped<IInvoiceService, InvoiceService>();
            services.AddScoped<IInvoiceData, InvoiceData>();
            services.AddScoped<ICommanData, CommanData>();
            services.AddScoped<ICommanService, CommanService>();
            services.AddScoped<IMeetingData, MeetingData>();
            services.AddScoped<IMeetingService, MeetingService>();

            services.AddScoped<IDocumentService, DocumentService>();
            services.AddScoped<IDocumentData, DocumentData>();

            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                builder =>
                {
                    builder
                            .AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                });
            });

            // Register the Swagger Generator service. This service is responsible for genrating Swagger Documents.
            // Note: Add this service at the end after AddMvc() or AddMvcCore().
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "ES Home Care API",
                    Version = "v1",
                    Description = "Description for the API goes here.",
                    Contact = new OpenApiContact
                    {
                        Name = "ES Home Care",
                        Email = string.Empty,
                        Url = new Uri("http://eshomecareapi-prod.us-east-1.elasticbeanstalk.com/"),
                    },
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(MyAllowSpecificOrigins);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ES Home Care API V1");

                // To serve SwaggerUI at application's root page, set the RoutePrefix property to an empty string.
                c.RoutePrefix = string.Empty;
            });
        }
    }
}

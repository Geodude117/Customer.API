using Customer.API.Validators;
using Customer.Business.Address;
using Customer.Business.ContactInformation;
using Customer.Business.Customer;
using Customer.Repository;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Customer.API
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
            services.AddTransient<IUnitOfWork>(connection => new UnitOfWork(Configuration.GetConnectionString("DbConnection")));
            services.AddTransient<IAddressBusiness, AddressBusiness>();
            services.AddTransient<IContactInfomationBusiness, ContactInfomationBusiness>();
            services.AddTransient<ICustomerBusiness, CustomerBusiness>();
            
            services.AddTransient<IValidator<Models.Customer>, CustomerValidator>();
            services.AddTransient<IValidator<Models.ContactInformation>, ContactInformationValidator>();

            services.AddSingleton<IConfiguration>(x => Configuration);

            services.AddMvc(setup => {
                //...mvc setup...
            }).AddFluentValidation();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Customer.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Customer.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

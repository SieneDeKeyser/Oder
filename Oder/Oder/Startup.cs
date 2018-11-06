
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using NJsonSchema;
using NSwag.AspNetCore;
using Oder.Domain.Customers;
using Oder.Domain.Items;
using Oder.Domain.Orders;
using Oder.Services;
using Oder.Services.Customers;
using Oder.Services.ItemGroups;
using Oder.Services.Items;
using Oder.Services.Orders;

namespace Oder
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddAuthentication("BasicAuthentication")
                    .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

            services.AddSingleton<ICustomerRepository, CustomerRepository>()
                    .AddSingleton<ICustomerService, CustomerService>()
                    .AddSingleton<ICustomerMapper, CustomerMapper>();

            services.AddSingleton<IItemRepository, ItemRepository>()
                    .AddSingleton<IItemService, ItemService>()
                    .AddSingleton<IItemMapper, ItemMapper>();

            services.AddSingleton<IOrderService, OrderService>()
                    .AddSingleton<IOrderMapper, OrderMapper>()
                    .AddSingleton<IItemGroupMapper, ItemGroupMapper>()
                    .AddSingleton<IOrderRepository, OrderRepository>();

                    
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseAuthentication();

            app.UseSwaggerUi3WithApiExplorer(settings =>
            {
                settings.GeneratorSettings.DefaultPropertyNameHandling =
                    PropertyNameHandling.CamelCase;
            });

            app.UseMvc();

            app.Run(async context =>
            {
                context.Response.Redirect("/swagger");
            });
        }
    }
}

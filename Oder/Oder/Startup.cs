using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
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

            //app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}

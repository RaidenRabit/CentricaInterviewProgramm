using InternalAPI.DataAccess.DataAccessClasses;
using InternalAPI.DataAccess.IDataAccess;
using InternalAPI.DataManagement.DataManagementClasses;
using InternalAPI.DataManagement.IDataManagement;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace InternalAPI
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
            services.AddControllers();
            services.AddTransient<IDbDistrict, DbDistrict>();
            services.AddTransient<IDbRelationType, DbRelationType>();
            services.AddTransient<IDbSalesPerson, DbSalesPerson>();
            services.AddTransient<IDbSalesPersonToDistrict, DbSalesPersonToDistrict>();

            services.AddTransient<IDmDistrict, DmDistrict>();
            services.AddTransient<IDmSalesPersonToDistrict, DmSalesPersonToDistrict>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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

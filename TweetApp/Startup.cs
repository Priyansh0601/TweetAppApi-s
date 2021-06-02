using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweetApp.Repository;
using TweetApp.Service;

namespace TweetApp
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<ITweetQueries, TweetQueries>();
            services.AddScoped<ITweetService, TweetService>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("Tweet",
new OpenApiInfo { Title = "TweetApp", Version = "Tweet" });
            });
            services.AddControllers().AddNewtonsoftJson();

            services.AddCors(options => {
                options.AddPolicy("MODPolicy", builder => {
                    builder.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200").AllowAnyOrigin();
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //app.UseCors();
            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/Tweet/swagger.json", "TweetApp"); });
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseCors("MODPolicy");
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
               
            });
        }
    }
}

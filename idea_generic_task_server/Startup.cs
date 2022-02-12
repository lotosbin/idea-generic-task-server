﻿using idea_generic_task_server.Core;
using idea_generic_task_server.Core.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using zentao.client.Core;

namespace idea_generic_task_server; 

public class Startup {
    public Startup(IConfiguration configuration) {
        Configuration = configuration;
    }

    // ReSharper disable once InconsistentNaming
    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services) {
        services.AddAutoMapper(typeof(Task), typeof(TaskItem));
        services.AddMemoryCache();
        services.AddTaskService();
        services.AddControllers();
        services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo {Title = "idea_generic_task_server", Version = "v1"}); });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
        if (env.IsDevelopment()) {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "idea_generic_task_server v1"));
        }

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}
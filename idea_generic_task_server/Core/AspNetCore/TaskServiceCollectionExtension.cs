using idea_generic_task_server.Data;
using idea_generic_task_server.Data.Core;
using idea_generic_task_server.Data.Zentao;
using Microsoft.Extensions.DependencyInjection;
using zentao.client.Core.AspNetCore;

namespace idea_generic_task_server.Core.AspNetCore;

public static class TaskServiceCollectionExtension {
    // ReSharper disable once UnusedMethodReturnValue.Global
    public static IServiceCollection AddTaskService(this IServiceCollection services) {
        services.AddZentaoClient();
        services.AddTransient<IAsTaskService, TaskAsTaskService>();
        services.AddTransient<IAsTaskService, BugAsTaskService>();
        services.AddTransient<ITaskService, TaskReduceService>();
        return services;
    }
}
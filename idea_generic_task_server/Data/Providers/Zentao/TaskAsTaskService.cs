using System.Linq;
using AutoMapper;
using idea_generic_task_server.Core;
using zentao.client.Core;

namespace idea_generic_task_server.Data.Zentao;

internal class TaskAsTaskService : AsTaskServiceBase {
    public TaskAsTaskService(IZentaoClient zentaoClient, IMapper mapper) : base(zentaoClient, mapper) {
    }

    public override IQueryable<Task> GetMyTaskQueryable(string host, string username, string password) {
        return ZentaoClient.GetMyTaskAsync(host, username, password).Result.Select(item => {
            var task = Mapper.Map<Task>(item);
            task.issueUrl = $"{host}/task-view-{item.id}.html";
            return task;
        }).AsQueryable();
    }

    public override IQueryable<Task> GetTaskQueryable(string host, string username, string password) {
        //todo
        return ZentaoClient.GetMyTaskAsync(host, username, password).Result.Select(item => {
            var task = Mapper.Map<Task>(item);
            task.issueUrl = $"{host}/task-view-{item.id}.html";
            return task;
        }).AsQueryable();
    }
}
using System.Linq;
using AutoMapper;
using idea_generic_task_server.Core;
using zentao.client.Core;

namespace idea_generic_task_server.Data.Zentao;

internal class BugAsTaskService : AsTaskServiceBase {
    public BugAsTaskService(IZentaoClient zentaoClient, IMapper mapper) : base(zentaoClient, mapper) {
    }

    public override IQueryable<Task> GetMyTaskQueryable(string host, string username, string password) {
        return ZentaoClient.GetMyBugAsync(host, username, password).Result.Select(item => {
            var bug = Mapper.Map<Task>(item);
            bug.issueUrl = $"{host}/bug-view-{item.id}.html";
            return bug;
        }).AsQueryable();
    }

    public override IQueryable<Task> GetTaskQueryable(string host, string username, string password) {
        //todo
        return ZentaoClient.GetMyBugAsync(host, username, password).Result.Select(item => {
            var bug = Mapper.Map<Task>(item);
            bug.issueUrl = $"{host}/bug-view-{item.id}.html";
            return bug;
        }).AsQueryable();
    }
}
using System.Collections.Generic;
using System.Linq;
using idea_generic_task_server.Core;

namespace idea_generic_task_server.Data.Core;

internal class TaskReduceService : ITaskService {
    private readonly IEnumerable<IAsTaskService> _asTaskServices;

    public TaskReduceService(IEnumerable<IAsTaskService> asTaskServices) {
        _asTaskServices = asTaskServices;
    }

    public IQueryable<Task> GetMyTaskQueryable(string host, string username, string password) {
        return _asTaskServices.SelectMany(s => s.GetMyTaskQueryable(host, username, password).ToList()).AsQueryable();
    }

    public IQueryable<Task> GetTaskQueryable(string host, string username, string password) {
        return _asTaskServices.SelectMany(s => s.GetTaskQueryable(host, username, password).ToList()).AsQueryable();
    }
}
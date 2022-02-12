using System.Linq;
using idea_generic_task_server.Core;

namespace idea_generic_task_server.Data;

internal interface IAsTaskService {
    IQueryable<Task> GetMyTaskQueryable(string host, string username, string password);
    IQueryable<Task> GetTaskQueryable(string host, string username, string password);
}
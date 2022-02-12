using System.Linq;

namespace idea_generic_task_server.Core;

public interface ITaskService {
    IQueryable<Task> GetMyTaskQueryable(string host, string username, string password);
    IQueryable<Task> GetTaskQueryable(string host, string username, string password);
}
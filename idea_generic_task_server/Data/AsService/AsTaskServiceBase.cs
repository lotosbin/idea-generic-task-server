using System.Linq;
using AutoMapper;
using idea_generic_task_server.Core;
using zentao.client.Core;

namespace idea_generic_task_server.Data;

internal abstract class AsTaskServiceBase : IAsTaskService {
    protected readonly IZentaoClient ZentaoClient;
    protected readonly IMapper Mapper;

    public AsTaskServiceBase(IZentaoClient zentaoClient, IMapper mapper) {
        ZentaoClient = zentaoClient;
        Mapper = mapper;
    }
    
    public abstract IQueryable<Task> GetMyTaskQueryable(string host, string username, string password);
    public abstract IQueryable<Task> GetTaskQueryable(string host, string username, string password);
}
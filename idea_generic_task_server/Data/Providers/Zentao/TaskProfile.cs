using AutoMapper;
using idea_generic_task_server.Core;
using zentao.client.Core;

namespace idea_generic_task_server.Data.Zentao; 

public class TaskProfile : Profile {
    public TaskProfile() {
        var map = CreateMap<TaskItem, Task>();
        map.ForMember(dest => dest.summary, opt => opt.MapFrom(src => $"{src.projectName} {src.name}"));
        map.ForMember(dest => dest.closed, opt => opt.MapFrom(src => src.status == "done"));
        map.ForMember(dest => dest.type, opt => opt.MapFrom(src => "Task"));
    }
}
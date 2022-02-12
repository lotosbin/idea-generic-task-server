using System;
using System.Collections.Generic;
using System.Linq;
using idea_generic_task_server.Auth;
using idea_generic_task_server.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace idea_generic_task_server.Controllers;

/// <summary>
/// </summary>
/// <seealso>
///     <cref>https://support.atlassian.com/jira-software-cloud/docs/advanced-search-reference-jql-fields/#Advancedsearchingfieldsreference-TypeType</cref>
/// </seealso>
[ApiController]
[Route("[controller]")]
public class TaskController : ControllerBase {
    private readonly ILogger<TaskController> _logger;
    private readonly string _host;
    private readonly ITaskService _taskService;

    public TaskController(IConfiguration configuration, ILogger<TaskController> logger, ITaskService taskService) {
        _host = configuration.GetValue<String>("ASPNETCORE_ZENTAO_HOST");
        _logger = logger;
        _taskService = taskService;
    }

    /**
         * @see https://www.zentao.net/book/xtask.api/378.html
         */
    [HttpGet]
    public System.Threading.Tasks.Task<IEnumerable<Task>> Get(string jql = "") {
        // ReSharper disable once TemplateIsNotCompileTimeConstantProblem
        _logger.LogInformation($"get jql={jql}");

        var (username, password) = this.ParseUsernamePassword();
        //todo JQL(jira query language) 解析
        IQueryable<Task> queryable;
        if (jql.Contains("assignee = currentUser()")) {
            queryable = _taskService.GetMyTaskQueryable(_host, username, password);
        }
        else {
            queryable = _taskService.GetTaskQueryable(_host, username, password);
        }

        if (jql.Contains("resolution = unresolved")) {
            queryable = queryable.Where(t => !t.closed);
        }

        var tasks = queryable.ToList();
        return System.Threading.Tasks.Task.FromResult<IEnumerable<Task>>(tasks);
    }
}
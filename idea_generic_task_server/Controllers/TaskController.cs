using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using idea_generic_task_server.Auth;
using idea_generic_task_server.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using zentao.client.Core;

namespace idea_generic_task_server.Controllers {
    /// <summary>
    /// </summary>
    /// <seealso>
    ///     <cref>https://support.atlassian.com/jira-software-cloud/docs/advanced-search-reference-jql-fields/#Advancedsearchingfieldsreference-TypeType</cref>
    /// </seealso>
    [ApiController]
    [Route("[controller]")]
    public class TaskController : ControllerBase {
        private readonly IMapper _mapper;
        private readonly ILogger<TaskController> _logger;
        private readonly string _host;
        private readonly IZentaoClient _zentaoClient;

        public TaskController(IConfiguration configuration, IMapper mapper,
            ILogger<TaskController> logger, IZentaoClient zentaoClient) {
            _host = configuration.GetValue<String>("ASPNETCORE_ZENTAO_HOST");
            _mapper = mapper;
            _logger = logger;
            _zentaoClient = zentaoClient;
        }

        /**
         * @see https://www.zentao.net/book/xtask.api/378.html
         */
        [HttpGet]
        public async System.Threading.Tasks.Task<IEnumerable<Task>> Get(String jql) {
            // ReSharper disable once TemplateIsNotCompileTimeConstantProblem
            _logger.LogInformation($"get jql={jql}");
            //todo support jql
            var (username, password) = this.ParseUsernamePassword();
            var items = (await _zentaoClient.GetMyTaskAsync(_host, username, password)).Select(item => {
                    var task = _mapper.Map<Task>(item);
                    task.issueUrl = $"{_host}/task-view-{task.id}.html";
                    return task;
                })
                .Where(item => !item.closed);
            var bugs = (await _zentaoClient.GetMyBugAsync(_host, username, password)).Select(item => {
                    var bug = _mapper.Map<Task>(item);
                    bug.issueUrl = $"{_host}/bug-view-{bug.id}.html";
                    return bug;
                })
                .Where(item => !item.closed);
            return bugs.Concat(items);
        }
    }
}
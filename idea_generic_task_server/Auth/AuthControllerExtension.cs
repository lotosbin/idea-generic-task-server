using System;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace idea_generic_task_server.Auth;

internal static class AuthControllerExtension {
    public static (string username, string password) ParseUsernamePassword(this ControllerBase controller) {
        var authHeader = AuthenticationHeaderValue.Parse(controller.Request.Headers["Authorization"]);
        var credentialBytes = Convert.FromBase64String(authHeader.Parameter ?? "");
        var credentials = Encoding.UTF8.GetString(credentialBytes).Split(new[] {':'}, 2);
        var username = credentials[0];
        var password = credentials[1];
        return (username, password);
    }
}
using Microsoft.Extensions.Configuration;
using Octokit;
using System;
using Octokit.Webhooks.AspNetCore;
using Octokit.Webhooks.AzureFunctions;
using System.Text;
using Octokit.Webhooks;
using Microsoft.AspNetCore.Builder;
using CommitScraper;
using Microsoft.AspNetCore.Http;
using System.IO;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<WebhookEventProcessor, MyWebhookEventProcessor>();

builder.Services.AddControllers();

var app = builder.Build();

app.UseRouting();

app.MapControllers();

app.UseEndpoints(endpoints =>
{
    endpoints.MapGitHubWebhooks();
});

app.Run();
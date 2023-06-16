using Microsoft.AspNetCore.Mvc;
using CommitScraper.Models;
using Octokit;
using Octokit.Webhooks;
using Octokit.Webhooks.Events;
using Octokit.Webhooks.Events.IssueComment;
using DotNetEnv;
using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;

namespace CommitScraper.Controllers;

[ApiController]
[Route("[controller]")]
public class WebHookController : ControllerBase
{
  private readonly MyWebhookEventProcessor _processor;
  private readonly GitHubClient _gitHubClient;
  private readonly ILogger<WebHookController> _logger;
  private const string ShaPrefix = "sha=";
  public WebHookController(MyWebhookEventProcessor processor, IConfiguration configuration, ILogger<WebHookController> logger)
  {
    // GitHubClient gitHubClient
    string appId = Environment.GetEnvironmentVariable("APP_ID");
    string webhookSecret = Environment.GetEnvironmentVariable("WEBHOOK_SECRET");
    string privateKeyPath = Environment.GetEnvironmentVariable("PRIVATE_KEY_PATH");
    var gitHubToken = Environment.GetEnvironmentVariable("GITHUB_TOKEN");

    _logger = logger;

    _processor = processor;

    _gitHubClient = new GitHubClient(new ProductHeaderValue("CommitScraper"))
    {
      Credentials = new Credentials(gitHubToken)
    };
  }

  [HttpPost]
  public async Task<IActionResult> ProcessCommit([FromBody] PayloadModel payload)
  {
    {
      if (payload != null)
      {
        foreach (var commit in payload.Commits)
        {
          var commitSHA = commit.Id;
          var commitDetails = await _gitHubClient.Repository.Commit.Get(payload.Repository.Owner.Login, payload.Repository.Name, commitSHA);

          var commitMessage = commitDetails.Commit.Message;
          var committer = commitDetails.Commit.Committer;

          _logger.LogInformation($"Committer: {committer.Name}");
          _logger.LogInformation($"Commit message: {commitMessage}");
        }
        return Ok();
      }
      else
      {
        return NotFound();
      }
    }
  }
}
using Microsoft.AspNetCore.Mvc;
using CommitScraper.Models;
using Octokit;

namespace CommitScraper.Controllers;

[ApiController]
[Route("[controller]")]
public class WebHookController : ControllerBase
{
  private readonly MyWebhookEventProcessor _processor;
  private readonly GitHubClient _gitHubClient;
  private readonly ILogger<WebHookController> _logger;
  public WebHookController(MyWebhookEventProcessor processor, GitHubClient gitHubClient, IConfiguration configuration, ILogger<WebHookController> logger)
  {
    string appId = configuration["APP_ID"];
    string webhookSecret = configuration["WEBHOOK_SECRET"];
    string privateKeyPath = configuration["PRIVATE_KEY_PATH"];
    var gitHubToken = configuration["GITHUB_TOKEN"];

    _processor = processor;

    _gitHubClient = new GitHubClient(new ProductHeaderValue("CommitScraper"))
    {
      Credentials = new Credentials(gitHubToken)
    };
    _logger = logger;
  }

  [HttpPost]
  public async Task<IActionResult> ProcessCommit([FromBody] PayloadModel payload)
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
}


using Microsoft.AspNetCore.Mvc;
using CommitScraper.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Octokit;
using Microsoft.Extensions.Configuration;

namespace CommitScraper.Controllers;

[ApiController]
[Route("[controller]")]
public class WebHookController : ControllerBase
{
  private readonly MyWebhookEventProcessor _processor;
  private readonly GitHubClient _gitHubClient;
  public WebHookController(MyWebhookEventProcessor processor, GitHubClient gitHubClient, IConfiguration configuration)
  {
    string appId = configuration["APP_ID"];
    string webhookSecret = configuration["WEBHOOK_SECRET"];
    string privateKeyPath = configuration["PRIVATE_KEY_PATH"];
    var gitHubToken = configuration["GITHUB_TOKEN"];

    _processor = processor;

    _gitHubClient = new GitHubClient(new ProductHeaderValue("CommitScraper"))
    {
      Credentials = new Credentials("gitHubToken")
    };
  }

  [HttpGet]
  public async Task<IActionResult> Get([FromBody] PayloadModel payload)
  {

    
    foreach (var commit in payload.Commits)
    {
      var commitSHA = commit.Id;
      var commitDetails = await _gitHubClient.Repository.Commit.Get(payload.Repository.Owner.Login, payload.Repository.Name, commitSHA);

      var commitMessage = commitDetails.Commit.Message;
      var committer = commitDetails.Commit.Committer;

      // REWRITE THIS TO SEND OFF INSTEAD OF COMMAND LINE...
      System.Console.WriteLine($"Committer: {committer.Name}");
      System.Console.WriteLine($"Commit message: {commitMessage}");
    }
    return Ok();
  }
}


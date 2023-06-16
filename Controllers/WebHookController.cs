// using Microsoft.AspNetCore.Mvc;
// using CommitScraper.Models;
// using Octokit;
// using DotNetEnv;

// namespace CommitScraper.Controllers;

// [ApiController]
// [Route("[controller]")]
// public class WebHookController : ControllerBase
// {
//   private readonly MyWebhookEventProcessor _processor;
//   private readonly GitHubClient _gitHubClient;
//   private readonly ILogger<WebHookController> _logger;
//   public WebHookController(MyWebhookEventProcessor processor, IConfiguration configuration, ILogger<WebHookController> logger)
//   {
//     // GitHubClient gitHubClient
//     string appId = Environment.GetEnvironmentVariable("APP_ID");
//     string webhookSecret = Environment.GetEnvironmentVariable("WEBHOOK_SECRET");
//     string privateKeyPath = Environment.GetEnvironmentVariable("PRIVATE_KEY_PATH");
//     var gitHubToken = Environment.GetEnvironmentVariable("GITHUB_TOKEN");

//     _processor = processor;

//     _gitHubClient = new GitHubClient(new ProductHeaderValue("CommitScraper"))
//     {
//       Credentials = new Credentials(gitHubToken)
//     };
//     _logger = logger;
//   }

//   [HttpPost]
//   public async Task<IActionResult> ProcessCommit([FromBody] PayloadModel payload)
//   {
//     if (payload != null)
//     {
//       foreach (var commit in payload.Commits)
//       {
//         var commitSHA = commit.Id;
//         var commitDetails = await _gitHubClient.Repository.Commit.Get(payload.Repository.Owner.Login, payload.Repository.Name, commitSHA);

//         var commitMessage = commitDetails.Commit.Message;
//         var committer = commitDetails.Commit.Committer.Name;

//         _logger.LogInformation($"Committer: {committer}");
//         _logger.LogInformation($"Commit message: {commitMessage}");
//       }
//       return Ok();
//     }
//     else
//     {
//       return NotFound();
//     }
//   }
// }


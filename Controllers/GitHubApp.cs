using Microsoft.AspNetCore.Mvc;
using CommitScraper.Models;

namespace CommitScraper.Controllers;

[ApiController]
[Route("[controller]")]
public class GitHubAppController : ControllerBase {

[HttpGet("/{accountId}")]
public async Task<ActionResult>
}


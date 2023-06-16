using Octokit;
using System;
using Octokit.Webhooks.AspNetCore;
using Octokit.Webhooks.AzureFunctions;
using System.Text;
using DotNetEnv;
using CommitScraper;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<MyWebhookEventProcessor, MyWebhookEventProcessor>();

builder.Services.AddSingleton<GitHubClient>(serviceProvider =>
{
    var configuration = serviceProvider.GetRequiredService<IConfiguration>();
    var gitHubToken = Environment.GetEnvironmentVariable("GITHUB_TOKEN");
    
    return new GitHubClient(new ProductHeaderValue("CommitScraper"))
    {
        Credentials = new Credentials(gitHubToken)
    };
});

builder.Services.AddMvc();
builder.Services.AddOptions();
builder.Services.Configure<TokenOptions>(Configuration);


var app = builder.Build();

app.UseHttpsRedirection();
    
app.UseRouting();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseEndpoints(endpoints =>
{
    endpoints.MapGitHubWebhooks();
});


app.Map("/api/webhook", app =>
{
    app.Run(async context =>
    {
        // Handle the webhook request here
        // You can access the payload using context.Request.Body stream or model binding

        // Example:
        var payload = await context.Request.BodyReader.ReadAsync();
        var payloadString = Encoding.UTF8.GetString(payload.Buffer);
        Console.WriteLine(payloadString);

        // Respond with a 200 OK status code
        context.Response.StatusCode = 200;
        await context.Response.CompleteAsync();
    });
});

app.UseAuthorization();

app.MapControllers();

app.Run();

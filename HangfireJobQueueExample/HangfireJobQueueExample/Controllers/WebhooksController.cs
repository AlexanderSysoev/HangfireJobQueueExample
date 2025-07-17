using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HangfireJobQueueExample.Controllers;

[AllowAnonymous]
[ApiController]
[Route("[controller]")]
public class WebhooksController : ControllerBase
{
    [HttpPost]
    public IActionResult Schedule([FromQuery] int count)
    {
        Parallel.ForEach(Enumerable.Range(0, count),
            new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount },
            i =>
            {
                BackgroundJob.Schedule<WebhookSendJob>(j => j.SendWebhookAsync(Guid.NewGuid()),
                    TimeSpan.FromSeconds(2));
            });

        return Ok();
    }
}
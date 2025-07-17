using Hangfire;

namespace HangfireJobQueueExample;

public class WebhookSendJob
{
    [Queue("webhooks")]
    [AutomaticRetry(DelaysInSeconds = 
        [
            1,
            2,
            4,
            6,
            8,
            10
        ],
        OnAttemptsExceeded = AttemptsExceededAction.Delete)]
    public async Task<string> SendWebhookAsync(Guid transactionId)
    {
        throw new ApplicationException("Sending webhook failed");
    }
}
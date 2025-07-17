using Hangfire;
using Hangfire.PostgreSql;
using HangfireJobQueueExample;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHangfire(config =>
    config.UsePostgreSqlStorage(c =>
        c.UseNpgsqlConnection(builder.Configuration.GetConnectionString("HangfireConnection"))));
builder.Services.AddHangfireServer(options =>
{
    options.Queues = ["default", "webhooks"];
});
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<WebhookSendJob>();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var app = builder.Build();

app.UseHangfireDashboard();
app.UseHangfireServer();
app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI();

app.Run();
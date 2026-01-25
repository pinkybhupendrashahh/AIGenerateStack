using AIGenerateStack.Server.Backgroundservices;
using AIGenerateStack.Server.Infrastructure;
using AIGenerateStack.Server.Middleware;
using Application.Interfaces;
using Application.UseCases;
using Domain.Models;
using Infrastructure.AI;
using Infrastructure.Audio;
using Infrastructure.Git;
using Infrastructure.Persistence;
using Infrastructure.Video;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddScoped<ITtsService, LocalTtsService>();
builder.Services.AddScoped<IVideoGenerator, AiVideoGenerator>();
//builder.Services.AddScoped<IVideoComposer, VideoComposer>();
builder.Services.AddScoped<IVideoRepository, InMemoryVideoRepository>();
builder.Services.AddScoped<IAiScriptService, OllamaService>();
builder.Services.AddScoped<GenerateAiVideoUseCase>();
builder.Services.Configure<OllamaOptions>(
builder.Configuration.GetSection("Ollama"));
builder.Services.Configure<GitHubOptions>(
    builder.Configuration.GetSection("GitHub"));
builder.Services.AddSingleton<ISceneParser, SceneParser>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpClient<GitHubUploader>();

builder.Services.AddSingleton<IVideoJobQueue, VideoJobQueue>();
builder.Services.AddHostedService<VideoJobWorker>();
builder.Services.AddSingleton<IFfmpegService, FfmpegService>();
builder.Services.AddSingleton<IFileSystem, WebFileSystem>();

builder.Services.AddSingleton<IVideoComposer, VideoComposer>();
builder.Services.Configure<HostOptions>(options =>
{
    options.BackgroundServiceExceptionBehavior =
        BackgroundServiceExceptionBehavior.Ignore;
});

builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReact", policy =>
    {
        policy
            .WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});
builder.Services.AddHttpClient<IAiScriptService, OllamaService>((sp, client) =>
{
    var options = sp.GetRequiredService<IOptions<OllamaOptions>>().Value;
    client.BaseAddress = new Uri(options.BaseUrl);
    client.Timeout = TimeSpan.FromSeconds(6000);
});


var app = builder.Build();
app.UseMiddleware<ExceptionMiddleware>();
app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowReact");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();

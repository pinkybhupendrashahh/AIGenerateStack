using AIGenerateStack.Server.Middleware;
using Application.Interfaces;
using Application.UseCases;
using Domain.Models;
using Infrastructure.AI;
using Infrastructure.Audio;
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
builder.Services.AddScoped<IVideoComposer, VideoComposer>();
builder.Services.AddScoped<IVideoRepository, VideoRepository>();
builder.Services.AddScoped<IAiScriptService, OllamaService>();
builder.Services.AddScoped<GenerateAiVideoUseCase>();
builder.Services.Configure<OllamaOptions>(
builder.Configuration.GetSection("Ollama"));

builder.Services.AddEndpointsApiExplorer();


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

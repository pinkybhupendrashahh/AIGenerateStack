namespace AIGenerateStack.Server.Infrastructure
{
    using Application.Interfaces;
    using Microsoft.AspNetCore.Hosting;

    public class WebFileSystem : IFileSystem
    {
        public string WebRootPath { get; }

        public WebFileSystem(IWebHostEnvironment env)
        {
            WebRootPath = env.WebRootPath;
        }
    }

}

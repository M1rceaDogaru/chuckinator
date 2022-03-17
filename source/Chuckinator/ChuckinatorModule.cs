using Autofac;
using Chuckinator.Models;
using Chuckinator.Services;
using Microsoft.Extensions.Configuration;

namespace Chuckinator
{
    internal class ChuckinatorModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var settings = config.GetRequiredSection("Settings").Get<Settings>();
            builder.Register(ctx => settings).AsSelf();
            builder.RegisterType<JokeRetriever>().As<IJokeRetriever>();
            builder.RegisterType<JokeService>().As<IJokeService>();
            builder.RegisterType<Application>().AsSelf();
        }
    }
}

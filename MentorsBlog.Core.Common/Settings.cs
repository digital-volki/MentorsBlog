using System;
using System.IO;
using MentorsBlog.Core.Common.Interfaces;
using Microsoft.Extensions.Configuration;

namespace MentorsBlog.Core.Common
{
    public class Settings : ISettings
    {
        public readonly IConfiguration Configuration;

        public Settings(string env = null)
        {
            var envKey = env is null ? EnvironmentGetter.Get() : EnvironmentGetter.Get(env);
            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile($"appsettings.{envKey}.json", true, true);
            Configuration = configurationBuilder.Build();
        }

        public string GetValue(string name)
        {
            return Configuration[name];
        }

        public T GetValue<T>(string name)
        {
            var value = Configuration[name];
            return (T)Convert.ChangeType(value, typeof(T));
        }

        public T GetSection<T>(string name) where T : class, new()
        {
            return Configuration.GetSection(name).Get<T>();
        }
    }
}
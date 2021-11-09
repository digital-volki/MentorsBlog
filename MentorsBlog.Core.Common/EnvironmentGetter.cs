using System;
using System.Collections.Generic;

namespace MentorsBlog.Core.Common
{
    public static class EnvironmentGetter
    {
        private const string DefaultEnvironmentName = "ASPNETCORE_ENVIRONMENT";

        private static readonly Dictionary<string, string> EnvValue = new()
        {
            { Environments.Production, nameof(Environments.Production) },
            { Environments.ProductionRemote, nameof(Environments.ProductionRemote) },
        };

        public static string Get(string key = Environments.Production)
        {
            var envVar = Environment.GetEnvironmentVariable(DefaultEnvironmentName) ?? key;

            return EnvValue.ContainsKey(envVar) 
                ? EnvValue[envVar]
                : throw new Exception($"Could not find environment by key `{envVar}`");
        }
    }

    public static class Environments
    {
        public const string Production = "prod";
        public const string ProductionRemote = "prod-remote";
    }
}
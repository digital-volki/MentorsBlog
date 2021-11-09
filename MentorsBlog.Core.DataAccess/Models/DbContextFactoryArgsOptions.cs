using CommandLine;

namespace MentorsBlog.Core.DataAccess.Models
{
    internal class DbContextFactoryArgsOptions
    {
        [Option(shortName: 'e', longName: "environment", Required = false, HelpText = "Environment", Default = "prod")]
        public string Environment { get; set; }
    }
}
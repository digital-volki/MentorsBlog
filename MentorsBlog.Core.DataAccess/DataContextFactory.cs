using CommandLine;
using MentorsBlog.Core.Common;
using MentorsBlog.Core.Common.Models;
using MentorsBlog.Core.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Logging;

namespace MentorsBlog.Core.DataAccess
{
    public class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        private static string EnvLocalKey => "local";

        public DataContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder();
            var settings = Parser.Default.ParseArguments<DbContextFactoryArgsOptions>(args)
                .MapResult(opts => new Settings(opts.Environment),
                    _ => new Settings(EnvLocalKey));

            var appSettings = settings.GetSection<AppSettings>(nameof(AppSettings));
            optionsBuilder.UseNpgsql(appSettings.ConnectionStrings.DbConnection);
            
            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.SetMinimumLevel(LogLevel.Trace);
            });
            var loggerDataContext = loggerFactory.CreateLogger<DataContext>();

            return new DataContext(optionsBuilder.Options, loggerDataContext);
        }
    }
}
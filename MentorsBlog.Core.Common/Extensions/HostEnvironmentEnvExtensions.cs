using System;
using Microsoft.Extensions.Hosting;

namespace MentorsBlog.Core.Common.Extensions
{
    public static class HostEnvironmentEnvExtensions
    {
        /// <summary>
        /// Checks if the current host environment name not isn't <see cref="Environments.Production"/>.
        /// </summary>
        /// <param name="hostEnvironment">An instance of <see cref="IHostEnvironment"/>.</param>
        /// <returns>True if the environment name isn't <see cref="Environments.Production"/>, otherwise false.</returns>
        public static bool IsLocal(this IHostEnvironment hostEnvironment)
        {
            if (hostEnvironment == null)
            {
                throw new ArgumentNullException(nameof(hostEnvironment));
            }

            return !hostEnvironment.IsEnvironment(Environments.Production);
        }
    }
}
using System;

namespace Gustnado.Tests
{
    public static class EnvironmentVariables
    {
        public static string ClientId { get; } = Get("Sharknado.Tests.ClientId");
        public static string ClientSecret { get; } = Get("Sharknado.Tests.ClientSecret");
        public static string Username { get; } = Get("Sharknado.Tests.Username");
        public static string Password { get; } = Get("Sharknado.Tests.Password");

        private static string Get(string variable) => Environment.GetEnvironmentVariable(variable, EnvironmentVariableTarget.Machine);
    }
}
using Serilog.Core;
using Serilog.Events;

namespace SocialMedia.Api.Configurations.Logs
{
    public class CustomUsernameColumn : ILogEventEnricher
    {
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            var (username, value) = logEvent.Properties.FirstOrDefault(x => x.Key == "UserName");
            if (value is not null)
            {
                var getValue = propertyFactory.CreateProperty(username, value);
                logEvent.AddPropertyIfAbsent(getValue);
            }
        }
    }
}

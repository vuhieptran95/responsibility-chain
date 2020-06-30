using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using Newtonsoft.Json;
using Notification.Web.Helpers;
using Notification.Web.ScheduledJobs;

namespace Notification.Web
{
    [SuppressMessage("ReSharper", "RCS1102")]
    public class AppSettings
    {
        private static TokenResponse _tokenResponse;
        public static Logging Logging { get; set; }
        public static ConnectionStrings ConnectionStrings { get; set; }
        public static string AllowedHosts { get; set; }
        public static JobSchedules JobSchedules { get; set; }
        public static MailSettings MailSettings { get; set; }
        public static ExternalServices ExternalServices { get; set; }

        public static async Task<TokenResponse> GetTokenResponse()
        {
            if (_tokenResponse == null)
            {
                
                    var idpTokenEndpoint = AppSettings.ExternalServices.IdP.Endpoint.AppendPathSegment("connect/token");

                    var data = new
                    {
                        scope = "phr.projects-not-yet-submit:read phr.projects-missed-deadline:read",
                        grant_type = "client_credentials"
                    };
                    var res = await idpTokenEndpoint.WithBasicAuth("hangfire", "secret")
                        .PostUrlEncodedAsync(data);

                    var tokenString = await res.Content.ReadAsStringAsync();

                    _tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(tokenString);
            }

            return _tokenResponse;
        }

        public static void ResetToken()
        {
            _tokenResponse = null;
        }
    }

    public class ConnectionStrings
    {
        public string HangfireConnection { get; set; }
    }

    public class JobSchedules
    {
        public ScheduleJob NotifyProjectsNotYetSubmittedWeeklyReport { get; set; }
        public ScheduleJob MarkProjectsAsMissedDeadline { get; set; }
        public ScheduleJob NotifyProjectsToDoDoDThisWeek { get; set; }
    }

    public class Logging
    {
        public LogLevel LogLevel { get; set; }
    }

    public class LogLevel
    {
        public string Default { get; set; }
        public string Microsoft { get; set; }
        public string MicrosoftHostingLifetime { get; set; }
    }

    public class MailSettings
    {
        public string HostName { get; set; }
        public int Port { get; set; }
        public string Mail { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool EnableSsl { get; set; }
        public MailList MailList { get; set; }
    }

    public class MailList
    {
        public string Pmo { get; set; }
        public string Coo { get; set; }
    }

    public class ScheduleJob
    {
        public string Cron { get; set; }
    }

    public class ExternalServices
    {
        public ExternalService PHR { get; set; }
        public ExternalService IdP { get; set; }
    }

    public class ExternalService
    {
        public string Endpoint { get; set; }
        public string AuthorizationHeader { get; set; }

        public async Task<IFlurlRequest> GetFlurlRequest()
        {
            var tokenResponse = await AppSettings.GetTokenResponse();
            
            return Endpoint.WithHeader("Content-Type", "application/json").WithOAuthBearerToken(tokenResponse.AccessToken);
        }
    }
}
using System;
using System.Collections.Generic;

namespace ProjectHealthReport.Domains.Helpers
{
    // TODO after removing DeliveryManagers member below, then rename this to public static class Roles, and remove Role- prefixes in the literal constants.
    public static class AuthorizationHelper
    {
        public const string RoleProjectManager = "Project Manager";
        public const string RoleSalesExecutives = "Sales Executive";
        public const string RoleGlobalExecutiveVicePresident = "Global Executive Vice President";
        public const string RoleDeliveryManager = "Delivery Manager";
        public const string RolePMOAssistant = "PMO Assistant";
        public const string RoleCOO = "COO";
        public const string RoleKam = "Key Account Manager";
        public const string RolePic = "Person In Charge";

        public const string ScopeUpdatePast = "weekly-report-past:update";
        public const string ScopeCreatePast = "weekly-report-past:create";
        
        public static List<(string, string)> UserRoleList
        {
            get
            {
                var list = new List<(string, string)>();
                list.AddRange(ProjectManagers);
                list.AddRange(ProjectManagementOffice);
                list.AddRange(KeyAccountManagers);
                list.AddRange(DeliveryManagerAccounts);
                return list;
            }
        }

        public static List<(string, string)> ProjectManagers = new List<(string, string)>
        {
            ("PM1", RolePic),
            ("PM2", RolePic),
            ("PM3", RolePic),
            ("PM4", RolePic),
            ("PM5", RolePic),
            ("PM6", RolePic),
            ("PM7", RolePic),
            ("PM8", RolePic),
            ("PM9", RolePic),
            ("PM10", RolePic),
            ("PM11", RolePic),
            ("PM12", RolePic),
            ("PM13", RolePic),
            ("PM14", RolePic),
            ("PM15", RolePic),
            ("PM16", RolePic),
            ("PM17", RolePic),
            ("PM18", RolePic),
            ("PM19", RolePic),
            ("PM20", RolePic),
        };
        
        public static List<(string, string)> KeyAccountManagers = new List<(string, string)>
        {
            ("KAM1", RoleKam),
            ("KAM2", RoleKam),
            ("KAM3", RoleKam),
            ("KAM4", RoleKam),
            ("KAM5", RoleKam),
        };
        
        public static List<(string, string)> ProjectManagementOffice = new List<(string, string)>
        {
            ("COO", RoleCOO),
            ("PMO1", RolePMOAssistant),
            ("PMO2", RolePMOAssistant),
            ("PMO3", RolePMOAssistant),
            ("PMO4", RolePMOAssistant),
        };
        
        public static List<(string, string)> DeliveryManagerAccounts = new List<(string, string)>
        {
            ("DM1", RoleDeliveryManager),
            ("DM2", RoleDeliveryManager),
            ("DM3", RoleDeliveryManager),
            ("DM4", RoleDeliveryManager),
            ("DM5", RoleDeliveryManager),
            ("DM6", RoleDeliveryManager),
            ("DM7", RoleDeliveryManager),
            ("DM8", RoleDeliveryManager),
        };

        // TODO refactor & remove
        public static Dictionary<string, string> DeliveryManagers = new Dictionary<string, string>
        {
            {"DM1", "AMS 24/7"},
            {"DM2", "Marketing"},
            {"DM3", "Frey"},
            {"DM4", "Baldur"},
            {"DM5", "HCMC"},
            {"DM6", "Thor"},
            {"DM7", "Odin"},
            {"DM8", "Tyr"},
        };

        public static List<string> AdministrationAcessRoles = new List<string>()
        {
            RolePMOAssistant, RoleCOO
        };
    }

    // TODO move into ProjectHealthReport.Web.Config
    // Not yet figuring out how, because some classes in .Features still reference this class
    public class AuthorizationRules
    {
        public PMsCanOnlyEditTheirReportsTill PMsCanOnlyEditTheirReportsTill { get; set; }
    }

    public class PMsCanOnlyEditTheirReportsTill
    {
        public int Hour { get; set; }
        public string Day { get; set; }
        public bool IsEnabled { get; set; }
    }

    public class BusinessRules
    {
        public ShowWarningIfProjectNotYetSubmitReport ShowWarningIfProjectNotYetSubmitReport { get; set; }
    }

    public class ShowWarningIfProjectNotYetSubmitReport
    {
        public string FromDay { get; set; }
        public int FromHour { get; set; }
        public string ToDay { get; set; }
        public int ToHour { get; set; }
        public int NumberOfWeekBetween { get; set; }
    }
}
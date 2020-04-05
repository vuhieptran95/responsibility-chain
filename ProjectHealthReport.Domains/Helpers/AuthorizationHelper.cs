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
        public const string RolePic = "Person In Charge";

        // TODO refactor & remove
        public static Dictionary<string, string> DeliveryManagers = new Dictionary<string, string>
        {
            {"NITECO\\hau.tran", "AMS 24/7"},
            {"NITECO\\tien.le2", "Marketing"},
            {"NITECO\\nam.trinh", "Frey" },
            {"NITECO\\tuan.nguyen3", "Baldur" },
            {"NITECO\\david.dwyer", "HCMC" },
            {"NITECO\\tung.nt", "Thor" },
            {"NITECO\\dung.le", "Odin" },
            {"NITECO\\giap.tran", "Tyr" },
            {"NITECO\\son.cao2", "Tyr" },
#if DEBUG
            {"NITECO\\hiep.tran2", "AMS 24/7" }
#endif
        };

        public static Dictionary<string, string> DeliveryManagerEmails = new Dictionary<string, string>
        {
            {"AMS 24/7", "hau.tran@niteco.se"},
            {"Marketing", "tien.le2@niteco.se"},
            {"Frey" ,"nam.trinh@niteco.se" },
            {"Baldur" ,"tuan.nguyen3@niteco.se"},
            {"Tyr","giap.tran@niteco.se" },
            {"HCMC","david.dwyer@niteco.se"},
            {"Thor","tung.nt@niteco.se" },
            {"Odin" ,"dung.le@niteco.se"},
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

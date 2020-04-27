using System.Collections.Generic;

namespace IdentityServer.Features
{
    public static class ApiResources
    {
        public const string Project = "project";
        public const string ProjectAccess = "project-access";
        public const string ProjectMaster = "project-master";
        public const string ProjectNonMaster = "project-non-master";
        public const string BacklogItem = "backlog-item";
        public const string QualityReport = "quality-report";
        public const string ProjectStatus = "project-status";
        public const string DoDReport = "dod-report";
        public const string ProjectAdmin = "project-admin";
        public const string Metrics = "metrics";
        public const string DivisionReport = "division-report";
    }

    public static class Actions
    {
        public const string Create = "create";
        public const string Read = "read";
        public const string Update = "update";
        public const string Delete = "delete";
        public const string All = "*";
    }

    public static class Roles
    {
        public const string RoleProjectManager = "Project Manager";
        public const string RoleSalesExecutives = "Sales Executive";
        public const string RoleGlobalExecutiveVicePresident = "Global Executive Vice President";
        public const string RoleDeliveryManager = "Delivery Manager";
        public const string RolePMOAssistant = "PMO Assistant";
        public const string RoleCOO = "COO";
        public const string RoleKam = "Key Account Manager";
        public const string RolePic = "Person In Charge";
        
        public static List<string> AssignableRoles = new List<string>()
        {
            RoleProjectManager, RoleDeliveryManager, RolePMOAssistant, RoleCOO, RoleKam, RolePic
        };
    }
}
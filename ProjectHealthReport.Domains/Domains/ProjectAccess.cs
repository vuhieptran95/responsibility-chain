namespace ProjectHealthReport.Domains.Domains
{
    public class ProjectAccess
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public Project Project { get; set; }
    }
}
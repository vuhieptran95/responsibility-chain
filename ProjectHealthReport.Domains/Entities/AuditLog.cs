using System;
using System.ComponentModel.DataAnnotations;

namespace ProjectHealthReport.Domains.Entities
{
    public class AuditLog
    {
        public int Id { get; set; }

        /// <summary>
        /// <para>Represents project ID, e.g.: <c>projects/1</c></para>
        /// <para>In the future, it might represent more entity types.</para>
        /// </summary>
        [MaxLength(63)]
        public string EntityId { get; set; }

        /// <summary>
        /// Represents the type of the command being tracked, e.g.: <c>ProjectHealthReport.Features.AddProject.AddProjectCommand</c>
        /// </summary>
        [MaxLength(255)]
        public string CommandType { get; set; }

        /// <summary>
        /// Represents the serialized command.
        /// </summary>
        [Required]
        public string CommandText { get; set; }

        /// <summary>
        /// Represents the (local) time right after the command is executed.
        /// </summary>
        public DateTime Recorded { get; set; }

        public string User { get; set; }
        public string Role { get; set; }
    }
}
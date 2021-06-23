namespace NowProvisionAPI.Core.Dtos.Agent
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class AgentDto 
    {
        public Guid AgentId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string Twitter { get; set; }
        public string Facebook { get; set; }
        public string LinkedIn { get; set; }
        public string License { get; set; }
        public string LicenseIcon { get; set; }
        public DateTimeOffset CreatedUtc { get; set; }
        public DateTimeOffset UpdatedUtc { get; set; }
        public DateTimeOffset DeletedUtc { get; set; }
        public bool? IsDeleted { get; set; }
        public string TenantId { get; set; }

        // add-on property marker - Do Not Delete This Comment
    }
}
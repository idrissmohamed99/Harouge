using System;
using System.Collections.Generic;

namespace Infra.Domain
{
    public partial class Screens
    {
        public string Id { get; set; }
        public string SName { get; set; }
        public string SerialNumber { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public string CaseId { get; set; }

        public virtual Cases Case { get; set; }
        public virtual Users User { get; set; }
    }
}

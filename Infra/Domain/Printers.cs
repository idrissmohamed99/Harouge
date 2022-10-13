using System;
using System.Collections.Generic;

namespace Infra.Domain
{
    public partial class Printers
    {
        public Printers()
        {
            Cases = new HashSet<Cases>();
        }

        public string Id { get; set; }
        public string PName { get; set; }
        public string SerialNumber { get; set; }
        public string TypeOfConnect { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdateAt { get; set; }

        public virtual Users User { get; set; }
        public virtual ICollection<Cases> Cases { get; set; }
    }
}

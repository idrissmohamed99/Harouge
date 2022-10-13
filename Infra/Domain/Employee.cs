using System;
using System.Collections.Generic;

namespace Infra.Domain
{
    public partial class Employee
    {
        public string Id { get; set; }
        public string EmployeName { get; set; }
        public string GeneralComment { get; set; }
        public string UserId { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public int PhoneNumber { get; set; }
        public int? RoomNumber { get; set; }
        public int BulidingId { get; set; }
        public string Administration { get; set; }
        public string InsideSite { get; set; }
        public string CaseId { get; set; }

        public virtual BuildingTypes Buliding { get; set; }
        public virtual Cases Case { get; set; }
        public virtual Users User { get; set; }
    }
}

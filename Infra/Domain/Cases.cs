using System;
using System.Collections.Generic;

namespace Infra.Domain
{
    public partial class Cases
    {
        public Cases()
        {
            Employee = new HashSet<Employee>();
            Screens = new HashSet<Screens>();
        }

        public string Id { get; set; }
        public string CName { get; set; }
        public string SerialNumber { get; set; }
        public string ComputerModel { get; set; }
        public string OperatingSystem { get; set; }
        public int TotalRam { get; set; }
        public int SizeHardDisk { get; set; }
        public int MsOfficeVs { get; set; }
        public string AntiVsType { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public string PrintId { get; set; }

        public virtual Printers Print { get; set; }
        public virtual Users User { get; set; }
        public virtual ICollection<Employee> Employee { get; set; }
        public virtual ICollection<Screens> Screens { get; set; }
    }
}

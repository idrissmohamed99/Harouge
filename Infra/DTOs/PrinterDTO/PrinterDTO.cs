using System;
using System.Collections.Generic;
using System.Text;

namespace Infra.DTOs.PrinterDTO
{
    public class PrinterDTO
    {

        public string Id { get; set; }
        public string PName { get; set; }
        public string SerialNumber { get; set; }
        public string TypeOfConnect { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdateAt { get; set; }

    }
}

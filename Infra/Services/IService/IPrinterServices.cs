using Infra.Domain;
using Infra.DTOs;
using Infra.DTOs.PrinterDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Services.IService
{
    public interface IPrinterServices
    {
        Task<bool> InsertPrinter(Printers Printers);
        Task<bool> UpdatePrinter(Printers Printers);
        Task<bool> DeletePrinter(string printerId);
        Task<Printers> GetById(string id);
        Task<PaginationDto<PrinterDTO>> GetAll(string pName, string SerialNumber, int pageNo, int pageSize);
      Task<List<PrinterAtciveDTO>> GetActivatePrinters(string printerName);
        Task<bool> ActivatePrinter(string id, bool isActive);
        Task<IQueryable<Printers>> FindPrinterBy(Expression<Func<Printers, bool>> predicate);

   
    }
}

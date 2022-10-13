using Infra;
using Infra.Domain;
using Infra.DTOs;
using Infra.DTOs.PrinterDTO;
using Infra.Services.IService;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class PrinterServices : IPrinterServices

    {
        private readonly IUnitOfWork _unitOfWork;

        public PrinterServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> ActivatePrinter(string id, bool isActive)
        {
            var printer = await _unitOfWork.GetRepository<Printers>().GetByID(id);
            printer.IsActive = !isActive;
            return true;
        }

        public async Task<bool> DeletePrinter(string printerId)
        {
            var printer = await _unitOfWork.GetRepository<Printers>().GetByID(printerId);
            await _unitOfWork.GetRepository<Printers>().Remove(printer);
            return true;
        }

        public async Task<IQueryable<Printers>> FindPrinterBy(Expression<Func<Printers, bool>> predicate)
      => await _unitOfWork.GetRepository<Printers>().FindBy(predicate);

        public async Task<List<PrinterAtciveDTO>> GetActivatePrinters(string printerName)
        {
            var printers = await _unitOfWork.GetRepository<Printers>().FindBy(pred =>
            pred.IsActive == true && (string.IsNullOrWhiteSpace(printerName) || pred.PName.StartsWith(printerName)));
            return await printers.Select(select => new PrinterAtciveDTO
            {

                PName = select.PName,
                Id = select.Id,
            }).ToListAsync();
        }

        public Task<PaginationDto<PrinterDTO>> GetAll(string pName, string SerialNumber, int pageNo, int pageSize)
        {
            throw new NotImplementedException();
        }

        public async Task<Printers> GetById(string id)
     => await _unitOfWork.GetRepository<Printers>().GetByID(id);

        public async Task<bool> InsertPrinter(Printers Printers)
        {
            await _unitOfWork.GetRepository<Printers>().Insert(Printers);
            return true;
        }

        public async Task<bool> UpdatePrinter(Printers Printers)
        {
            await _unitOfWork.GetRepository<Printers>().Update(Printers);
            return true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infra.ValidationServices
{
    public interface IprinterValidationServices
    {
        Task<bool> IsNameExist(string name);
        Task<bool> IsNameValid(string id,string name);
        Task<bool> IsDelete(string id);
        Task<bool> IsActive(string id);
    }
}

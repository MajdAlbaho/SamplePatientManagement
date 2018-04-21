using ClinicSystem.DataType.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicSystem.Model.RepositoryInterface
{
    public interface ICultureRepository : IReadOnlyRepository<Culture, int>
    {
        Task<string> GetCurrentCulture();
        Task SetCurrentCulture(Culture item);
    }
}

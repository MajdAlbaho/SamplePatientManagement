using ClinicSystem.DataType.Interfaces;
using ClinicSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicSystem.Model.RepositoryInterface
{
    public interface IDiagnosisRepository : ICrudRepository<Diagnosis, int>
    {

    }
}

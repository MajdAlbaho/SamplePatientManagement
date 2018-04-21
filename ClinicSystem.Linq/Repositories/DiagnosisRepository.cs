using ClinicSystem.Linq.LinqDb;
using ClinicSystem.Model.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicSystem.Linq.Repositories
{
    public class DiagnosisRepository : BaseRepository<Model.Diagnosis, Diagnosi, int>, IDiagnosisRepository
    {

    }
}

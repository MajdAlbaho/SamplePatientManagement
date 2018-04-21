using ClinicSystem.Model;
using ClinicSystem.Model.RepositoryInterface;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicSystem.Linq.Repositories
{
    public class CultureRepository : BaseReadOnlyRepository<Culture, LinqDb.Culture, int>, ICultureRepository
    {
        public Task<string> GetCurrentCulture()
        {
            return ExecuteOnDb(db =>
            {
                return db.Cultures.First(e => e.IsCurrent).Code;
            });
        }

        public Task SetCurrentCulture(Culture item)
        {
            return ExecuteOnDb(db =>
            {
                foreach (var culture in db.Cultures)
                {
                    if (culture.Code == item.Code)
                        culture.IsCurrent = true;
                    else
                        culture.IsCurrent = false;
                }
                db.SubmitChanges();
            });
        }
    }
}

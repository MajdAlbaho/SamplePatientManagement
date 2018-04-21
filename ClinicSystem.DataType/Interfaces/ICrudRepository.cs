using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicSystem.DataType.Interfaces
{
    public interface IReadOnlyRepository<TModel, TKey>
    {
        Task<IEnumerable<TModel>> GetItems();
        Task<TModel> FindById(TKey id);
    }

    public interface ICrudRepository<TModel, TKey> : IReadOnlyRepository<TModel, TKey>
    {
        Task<TModel> InsertItem(TModel item);
        Task<TModel> UpdateItem(TModel source);
        Task DeleteItem(TModel item);
        Task DeleteItem(TKey id);
    }
}

using ClinicSystem.DataType;
using ClinicSystem.DataType.Interfaces;
using ClinicSystem.Linq.LinqDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ClinicSystem.Linq
{
    public class BaseRepository
    {
        public Task ExecuteOnDb(Action<ClinicSystemDataContext> action)
        {
            return Task.Run(() =>
            {
                using (var db = new ClinicSystemDataContext())
                {
                    action?.Invoke(db);
                    db.SubmitChanges();
                }
            });
        }

        protected Task<TResult> ExecuteOnDb<TResult>(Func<ClinicSystemDataContext, TResult> action)
        {
            return Task.Run(() =>
            {
                using (var db = new ClinicSystemDataContext())
                {
                    var res = action(db);
                    db.SubmitChanges();
                    return res;
                }
            });
        }

    }

    public class BaseReadOnlyRepository<TModel, TTable, TKey> : BaseRepository
                                                    where TModel : class, IUniqueObject<TKey>, new()
                                                    where TTable : class, new()
    {
        public virtual Task<IEnumerable<TModel>> GetItems()
        {
            return ExecuteOnDb(db => Mapper.MapCollection<TModel>(db.GetTable<TTable>()).ToList().AsEnumerable());
        }

        public virtual Task<TModel> FindById(TKey id)
        {
            return ExecuteOnDb(db =>
            {
                var dbItem = GetItemByPrimaryKey(db, id);
                return Mapper.Map<TModel>(dbItem);
            });
        }

        protected TTable GetItemByPrimaryKey(ClinicSystemDataContext context, object pk)
        {
            var table = context.GetTable<TTable>();
            var mapping = context.Mapping.GetTable(typeof(TTable));
            var pkfield = mapping.RowType.DataMembers.SingleOrDefault(d => d.IsPrimaryKey);
            if (pkfield == null)
            {
                pkfield = mapping.RowType.DataMembers.SingleOrDefault(d => d.Name == "ID");
                if (pkfield == null)
                    throw new Exception($"Table {mapping.TableName} does not contain a Primary Key field");
            }

            var param = Expression.Parameter(typeof(TTable), "e");
            var predicate = Expression.Lambda<Func<TTable, bool>>
                (Expression.Equal(Expression.Property(param, pkfield.Name), Expression.Constant(pk)), param);
            var o = table.SingleOrDefault(predicate);
            return o;
        }
    }


    public class BaseRepository<TModel, TTable, TKey> : BaseReadOnlyRepository<TModel, TTable, TKey>
                                                    where TModel : class, IUniqueObject<TKey>, new()
                                                    where TTable : class, new()
    {
       
        public virtual Task<TModel> InsertItem(TModel item)
        {
            if (item == null)
                throw new Exception("Parameter can't be null.");

            return ExecuteOnDb(db =>
            {
                var dbItem = Mapper.Map<TTable>(item);
                db.GetTable<TTable>().InsertOnSubmit(dbItem);
                db.SubmitChanges();

                return Mapper.Map<TModel>(dbItem);
            });
        }

        public virtual Task<TModel> UpdateItem(TModel source)
        {
            if (source == null)
                throw new Exception("Parameter can't be null.");

            return ExecuteOnDb((db) =>
            {
                Mapper.Map(source, GetItemByPrimaryKey(db, source.GetKey()));
                return source;
            });
        }

        public virtual Task DeleteItem(TModel item)
        {
            if (item == null)
                throw new Exception("Parameter can't be null.");

            return ExecuteOnDb(db => DeleteItem(item.GetKey()));
        }

        public virtual Task DeleteItem(TKey id)
        {
            return ExecuteOnDb(db =>
            {
                var target = GetItemByPrimaryKey(db, id);
                if (target == null)
                    throw new Exception($"No such item found in table with ID : {id}");

                db.GetTable<TTable>().DeleteOnSubmit(target);
            });
        }
    }
}

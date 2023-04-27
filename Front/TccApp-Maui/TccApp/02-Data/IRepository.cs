using TccApp.Domain.Models;
using System.Linq.Expressions;

namespace TccApp.Data
{
    public interface IRepository<TModel> : IDisposable where TModel : BaseModel
    {
        bool Sucess { get; set; }
        string StatusMessage { get; set; }
        void Create(TModel item);
        void CreateWithChildren(TModel item, bool recursive = false);
        void Update(TModel item);
        void UpdateWithChildren(TModel item, bool recursive = false);
        void Delete(TModel item);
        void DeleteAll();

        TModel Get(Guid id);
        TModel Get(Expression<Func<TModel, bool>> predicate);
        List<TModel> GetAll();
        List<TModel> GetAll(Expression<Func<TModel, bool>> predicate);
        List<TModel> GetAllWithChildren();
        
        int Count();
        List<TModel> Sql(string sql);
    }
}

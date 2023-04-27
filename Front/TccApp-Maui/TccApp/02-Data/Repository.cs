using TccApp.Domain.Consts;
using TccApp.Domain.Models;
using SQLite;
using SQLiteNetExtensions.Extensions;
using System.Linq.Expressions;

namespace TccApp.Data
{
    public class Repository<TModel> : IRepository<TModel> where TModel : BaseModel, new()
    {
        private readonly SQLiteConnection database;
        //private readonly SQLiteAsyncConnection database;

        public bool Sucess { get; set; }
        public string StatusMessage { get; set; }

        public Repository()
        {
            database = new SQLiteConnection(ConfigApp.DatabasePath, ConfigApp.Flags);
            //database = new SQLiteAsyncConnection(ConfigApp.DatabasePath, ConfigApp.Flags);

            database.CreateTable<TModel>();
        }

        public void Dispose()
        {
            database.Close();
        }
        
        public void Create(TModel model)
        {
            try
            {
                Sucess = true;
                var result = database.Insert(model);
                StatusMessage = $"{result} registros(s) adicionado(s)";                
            }
            catch (Exception ex)
            {
                Sucess = false;
                StatusMessage = $"Erro: {ex.Message}";                
            }
        }

        public void CreateWithChildren(TModel model, bool recursive = false)
        {
            try
            {
                Sucess = true;
                database.InsertWithChildren(model, recursive);
            }
            catch (Exception ex)
            {
                Sucess = false;
                StatusMessage = $"Erro: {ex.Message}";
            }
        }

        public void Update(TModel model)
        {
            try
            {
                Sucess = true;
                var result = database.Update(model);
                StatusMessage = $"{result} registro(s) atualizado(s)";
            }
            catch (Exception ex)
            {
                Sucess = false;
                StatusMessage = $"Erro: {ex.Message}";
            }
        }

        public void UpdateWithChildren(TModel model, bool recursive = false)
        {
            try
            {
                database.UpdateWithChildren(model);
                Sucess = true;
            }
            catch (Exception ex)
            {
                Sucess = false;
                StatusMessage = $"Erro: {ex.Message}";
            }
        }

        public void Delete(TModel model)
        {
            try
            {
                Sucess = true;
                database.Delete(model, true);
            }
            catch (Exception ex)
            {
                Sucess = false;
                StatusMessage = $"Erro: {ex.Message}";
            }
        }

        public void DeleteAll()
        {
            try
            {
                Sucess = true;
                database.DeleteAll(database.Table<TModel>().Table);
            }
            catch (Exception ex)
            {
                Sucess = false;
                StatusMessage = $"Erro: {ex.Message}";
            }
        }

        public TModel Get(Guid id)
        {
            try
            {
                Sucess = true;
                return database.Table<TModel>().FirstOrDefault(x => x.Id == id);
            }
            catch (Exception ex)
            {
                Sucess = false;
                StatusMessage = $"Erro: {ex.Message}";
            }
            return null;
        }

        public TModel Get(Expression<Func<TModel, bool>> predicate)
        {
            try
            {
                Sucess = true;
                return database.Table<TModel>().Where(predicate).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Sucess = false;
                StatusMessage = $"Erro: {ex.Message}";
            }
            return null;
        }

        public List<TModel> GetAll()
        {
            try
            {
                Sucess = true;
                return database.Table<TModel>().ToList();
            }
            catch (Exception ex)
            {
                Sucess = false;
                StatusMessage = $"Erro: {ex.Message}";
            }
            return null;
        }

        public List<TModel> GetAll(Expression<Func<TModel, bool>> predicate)
        {
            try
            {
                Sucess = true;
                return database.Table<TModel>().Where(predicate).ToList();
            }
            catch (Exception ex)
            {
                Sucess = false;
                StatusMessage = $"Erro: {ex.Message}";
            }
            return null;
        }
        
        public List<TModel> GetAllWithChildren()
        {
            try
            {
                Sucess = true;
                return database.GetAllWithChildren<TModel>().ToList();
            }
            catch (Exception ex)
            {
                Sucess = false;
                StatusMessage = $"Erro: {ex.Message}";
            }
            return null;
        }

        public int Count()
        {
            try
            {
                Sucess = true;
                return database.Table<TModel>().Count();
            }
            catch (Exception ex)
            {
                Sucess = false;
                StatusMessage = $"Erro: {ex.Message}";
            }
            return 0;
        }

        public List<TModel> Sql(string sql)
        {
            try
            {
                Sucess = true;
                return database.Query<TModel>(sql);
            }
            catch (Exception ex)
            {
                Sucess = false;
                StatusMessage = $"Erro: {ex.Message}";
            }
            return null;
        }       
    }
}

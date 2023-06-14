using TccApp.Domain.Interfaces;
using TccApp.Domain.Models;
using TccApp.Infraestructure.Helpers;
using SQLite;
using SQLiteNetExtensions.Extensions;
using System.Linq.Expressions;

namespace TccApp.Services
{
    public class Repository<TModel> : IRepository<TModel> where TModel : BaseModel, new()
    {
        protected readonly SQLiteConnection Database;
        
        public bool Sucess { get; set; }
        public string StatusMessage { get; set; }

        public Repository()
        {
            var dbConn = ServiceHelper.GetService<DatabaseConnection>();
            Database = dbConn.Database;
            
            Database.CreateTable<TModel>();
        }

        public void Create(TModel model)
        {
            try
            {
                Sucess = true;
                var result = Database.Insert(model);
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
                Database.InsertWithChildren(model, recursive);
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
                var result = Database.Update(model);
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
                Database.UpdateWithChildren(model);
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
                Database.Delete(model, true);
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
                Database.DeleteAll(Database.Table<TModel>().Table);
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
                return Database.Table<TModel>().FirstOrDefault(x => x.Id == id);
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
                return Database.Table<TModel>().Where(predicate).FirstOrDefault();
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
                return Database.Table<TModel>().ToList();
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
                return Database.Table<TModel>().Where(predicate).ToList();
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
                return Database.GetAllWithChildren<TModel>().ToList();
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
                return Database.Table<TModel>().Count();
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
                return Database.Query<TModel>(sql);
            }
            catch (Exception ex)
            {
                Sucess = false;
                StatusMessage = $"Erro: {ex.Message}";
            }
            return null;


            //using (var cmd = new SQLiteCommand(DbConnection()))
            //{
            //    if (cliente.Id != null)
            //    {
            //        cmd.CommandText = "UPDATE Clientes SET Nome=@Nome, Email=@Email WHERE Id=@Id";
            //        cmd.Parameters.AddWithValue("@Id", cliente.Id);
            //        cmd.Parameters.AddWithValue("@Nome", cliente.Nome);
            //        cmd.Parameters.AddWithValue("@Email", cliente.Email);
            //        cmd.ExecuteNonQuery();
            //    }
            //};
        }       
    }
}

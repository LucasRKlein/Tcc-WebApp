using System.Threading.Tasks;
using Tcc.Persistence.Contextos;
using Tcc.Persistence.Interface;

namespace Tcc.Persistence
{
    public class GeralPersist : IGeralPersist
    {
        private readonly TccContext _context;
        public GeralPersist(TccContext context)
        {
            _context = context;

        }
        public void Add<T>(T entity) where T : class
        {
            _context.AddAsync(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public void DeleteRange<T>(T[] entityArray) where T : class
        {
            _context.RemoveRange(entityArray);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
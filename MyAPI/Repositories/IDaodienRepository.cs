using MyAPI.Models;

namespace MyAPI.Repositories
{
    public interface IDaodienRepository
    {
        public Task< List<DaodienModel>> GetAll();
        public Task<DaodienModel> GetByID(string id);
        public Task<string> Add(DaodienModel daodien);
        public Task Update(string id,DaodienModel daodien);
        public Task Delete(string id);
    }
}

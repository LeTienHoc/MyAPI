using MyAPI.Models;

namespace MyAPI.Repositories
{
    public interface IDienvienRepository
    {
        public Task< List<DienvienModel>> GetAll();
        public Task<DienvienModel> GetByID(string id);
        public Task<string> Add(DienvienModel Dienvien);
        public Task Update(string id,DienvienModel Dienvien);
        public Task Delete(string id);
    }
}

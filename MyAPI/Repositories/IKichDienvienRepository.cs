using MyAPI.Models;

namespace MyAPI.Repositories
{
    public interface IKichDienvienRepository
    {
        public Task< List<KichDienvienModel>> GetAll();
        public Task<KichDienvienModel> GetByID(string id);
        public Task<string> Add(KichDienvienModel KichDienvien);
        public Task Update(string id,KichDienvienModel KichDienvien);
        public Task Delete(string id);
    }
}

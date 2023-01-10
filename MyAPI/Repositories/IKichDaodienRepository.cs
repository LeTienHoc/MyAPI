using MyAPI.Models;

namespace MyAPI.Repositories
{
    public interface IKichDaodienRepository
    {
        public Task< List<KichDaodienModel>> GetAll();
        public Task<KichDaodienModel> GetByID(string id);
        public Task<string> Add(KichDaodienModel KichDaodien);
        public Task Update(string id,KichDaodienModel KichDaodien);
        public Task Delete(string id);
    }
}

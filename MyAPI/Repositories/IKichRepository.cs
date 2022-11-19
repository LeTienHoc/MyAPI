using MyAPI.Models;

namespace MyAPI.Repositories
{
    public interface IKichRepository
    {
        public Task< List<KichModel>> GetAll();
        public Task<KichModel> GetByID(string id);
        public Task<string> Add(KichModel Kich);
        public Task Update(string id,KichModel Kich);
        public Task Delete(string id);
    }
}

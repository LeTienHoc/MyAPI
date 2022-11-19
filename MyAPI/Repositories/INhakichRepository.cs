using MyAPI.Models;

namespace MyAPI.Repositories
{
    public interface INhakichRepository
    {
        public Task< List<NhakichModel>> GetAll();
        public Task<NhakichModel> GetByID(string id);
        public Task<string> Add(NhakichModel Nhakich);
        public Task Update(string id,NhakichModel Nhakich);
        public Task Delete(string id);
    }
}

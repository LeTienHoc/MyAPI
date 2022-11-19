using MyAPI.Models;

namespace MyAPI.Repositories
{
    public interface IGheRepository
    {
        public Task< List<GheModel>> GetAll();
        public Task<GheModel> GetByID(string id);
        public Task<string> Add(GheModel Ghe);
        public Task Update(string id,GheModel Ghe);
        public Task Delete(string id);
    }
}

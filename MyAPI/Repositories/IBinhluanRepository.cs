using MyAPI.Models;

namespace MyAPI.Repositories
{
    public interface IBinhluanRepository
    {
        public Task< List<BinhluanModel>> GetAll();
        public Task<BinhluanModel> GetByID(string id);
        public Task<string> Add(BinhluanModel Binhluan);
        public Task Update(string id,BinhluanModel Binhluan);
        public Task Delete(string id);
    }
}

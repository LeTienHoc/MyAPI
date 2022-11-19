using MyAPI.Models;

namespace MyAPI.Repositories
{
    public interface ITaikhoanRepository
    {
        public Task< List<TaikhoanModel>> GetAll();
        public Task<TaikhoanModel> GetByID(string id);
        public Task<string> Add(TaikhoanModel Taikhoan);
        public Task Update(string id,TaikhoanModel Taikhoan);
        public Task Delete(string id);
    }
}

using MyAPI.Models;

namespace MyAPI.Repositories
{
    public interface IKhachhangRepository
    {
        public Task< List<KhachhangModel>> GetAll();
        public Task<KhachhangModel> GetByID(string id);
        public Task<string> Add(KhachhangModel Khachhang);
        public Task Update(string id,KhachhangModel Khachhang);
        public Task Delete(string id);
    }
}

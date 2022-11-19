using MyAPI.Models;

namespace MyAPI.Repositories
{
    public interface IKhuyenmaiRepository
    {
        public Task< List<KhuyenmaiModel>> GetAll();
        public Task<KhuyenmaiModel> GetByID(string id);
        public Task<string> Add(KhuyenmaiModel Khuyenmai);
        public Task Update(string id,KhuyenmaiModel Khuyenmai);
        public Task Delete(string id);
    }
}

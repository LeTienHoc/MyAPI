using MyAPI.Models;

namespace MyAPI.Repositories
{
    public interface ILichchieuKhuyenmaiRepository
    {
        public Task< List<LichchieuKhuyenmaiModel>> GetAll();
        public Task<LichchieuKhuyenmaiModel> GetByID(string id);
        public Task<string> Add(LichchieuKhuyenmaiModel LichchieuKhuyenmai);
        public Task Update(string id,LichchieuKhuyenmaiModel LichchieuKhuyenmai);
        public Task Delete(string id);
    }
}

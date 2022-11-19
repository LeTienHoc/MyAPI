using MyAPI.Models;

namespace MyAPI.Repositories
{
    public interface ILichchieuRepository
    {
        public Task< List<LichchieuModel>> GetAll();
        public Task<LichchieuModel> GetByID(string id);
        public Task<string> Add(LichchieuModel Lichchieu);
        public Task Update(string id,LichchieuModel Lichchieu);
        public Task Delete(string id);
    }
}

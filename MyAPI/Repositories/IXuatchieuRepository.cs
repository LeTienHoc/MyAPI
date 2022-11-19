using MyAPI.Models;

namespace MyAPI.Repositories
{
    public interface IXuatchieuRepository
    {
        public Task< List<XuatchieuModel>> GetAll();
        public Task<XuatchieuModel> GetByID(string id);
        public Task<string> Add(XuatchieuModel Xuatchieu);
        public Task Update(string id,XuatchieuModel Xuatchieu);
        public Task Delete(string id);
    }
}

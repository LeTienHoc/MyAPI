using MyAPI.Data;
using MyAPI.Models;

namespace MyAPI.Repositories
{
    public interface IVeRepository
    {
        public Task< List<VeModel>> GetAll();
        public Task<VeModel> GetByID(string id);
        public Task<string> Add(VeModel Ve);
        public Task Update(string id,VeModel Ve);
        public Task Delete(string id);
     //   public Task SeatGhe(GheModel Ghe,int soluong);
    }
}

using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Mvc;
using MyAPI.Models;

namespace MyAPI.Repositories
{
    public interface IKichRepository
    {
        public  List<KichPageModel> GetAll();
        public Task<KichModel> GetByID(string id);
        public Task<string> Add(KichModel Kich);
        public Task Update(string id,KichModel Kich);
        public Task Delete(string id);
        public List<KichPageModel> Getallkichs(string search);
        public Task DuyetKich(string id,UpdateModel model);
        public List<KichPageModel> Detail(string id);
        
    }
}

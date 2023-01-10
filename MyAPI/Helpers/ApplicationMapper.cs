using AutoMapper;
using MyAPI.Data;
using MyAPI.Models;

namespace MyAPI.Helpers
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            CreateMap<Daodien, DaodienModel>().ReverseMap();

            CreateMap<Dienvien, DienvienModel>().ReverseMap();
            

            CreateMap<Ghe, GheModel>().ReverseMap();
            CreateMap<Khachhang, KhachhangModel>().ReverseMap();
            
            CreateMap<Kich, KichModel>().ReverseMap();
            CreateMap<KichDienvien, KichDienvienModel>().ReverseMap();
            CreateMap<KichDaodien, KichDaodienModel>().ReverseMap();
            CreateMap<Lichchieu, LichchieuModel>().ReverseMap();
            CreateMap<Nhakich, NhakichModel>().ReverseMap();
            CreateMap<Taikhoan, TaikhoanModel>().ReverseMap();
            CreateMap<Ve, VeModel>().ReverseMap();
            CreateMap<Kich, UpdateModel>().ReverseMap();
            CreateMap<Xuatchieu, XuatchieuModel>().ReverseMap();
        }
    }
}

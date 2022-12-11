using AutoMapper;
using AutoMapper.Internal.Mappers;
using System.Collections;

namespace MyAPI.Models
{
    public class ResponseSeat
    {
        private string seatHang;
        private ArrayList<ResponseTypeSeat> data;
        private readonly IMapper _mapper;

        public ResponseSeat(IMapper mapper)
        {
            data = new ArrayList<ResponseTypeSeat>();
            _mapper = mapper;
        }

        public ResponseSeat(string seatHang, Object data)
        {
            this.seatHang = seatHang;
            if (data != null)
            {
                //this.data = 
            }
            
        }

        private class ArrayList<T>
        {
        }
    }
}

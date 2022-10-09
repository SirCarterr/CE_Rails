using AutoMapper;
using DataAccess;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Train, TrainDTO>().ReverseMap();
            CreateMap<Stop, StopDTO>().ReverseMap();
            CreateMap<Wagon, WagonDTO>().ReverseMap();
            CreateMap<BookingDetail, BookingDetailDTO>().ReverseMap();
            CreateMap<BookingHeader, BookingHeaderDTO>().ReverseMap();
        }
    }
}

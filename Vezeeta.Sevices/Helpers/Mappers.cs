using AutoMapper;
using Vezeeta.Sevices.Models.DTOs;
using Vezeeta.Core.Models;
using Vezeeta.Core.Models.Users;
using Vezeeta.Services.Models.DTOs;

namespace Vezeeta.Sevices.Helpers
{
    public class Mappers :Profile
    {
        public Mappers()
        {
            CreateMap<AccountModelDto, ApplicationUser>().ReverseMap();
            CreateMap<CreateDoctorModelDto, ApplicationUser>().ReverseMap();
            CreateMap<CreateDoctorModelDto, Doctor>()
                .ForMember(dest=>dest.UserName, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest=>dest.Specialization, opt => opt.Ignore())
                 .AfterMap((src, dest, context) =>
                 {
                     dest.Photo = (string)context.Items["Photo"];
                 })
                .ReverseMap();
            CreateMap<AccountModelDto, Patient>()
                .ForMember(dest=>dest.UserName, opt => opt.MapFrom(src => src.Email))
                .AfterMap((src, dest, context) =>
                {
                    dest.Photo = (string)context.Items["Photo"];
                })
                .ReverseMap();
            CreateMap<CreatePatientModel, Patient>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
                .AfterMap((src, dest, context) =>
                {
                    dest.Photo = (string)context.Items["Photo"];
                })
                .ReverseMap();
            CreateMap<DiscountCodeDto, DiscountCode>().ReverseMap();
        }

    }
}


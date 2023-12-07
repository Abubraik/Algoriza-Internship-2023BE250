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
            CreateMap<DoctorModelDto, Doctor>()
                .ForMember(dest=>dest.UserName, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest=>dest.Specialization, opt => opt.Ignore())
                 .AfterMap((src, dest, context) =>
                 {
                     // Assuming you pass the Specialization object as a parameter when mapping
                     dest.Specialization = (Specialization)context.Items["Specialization"];
                 })
                .ReverseMap();
            //CreateMap<ApplicationUser, DoctorModelDto>().ReverseMap();
            //CreateMap<AccountModelDto, ApplicationUser>().ReverseMap();

            CreateMap<AccountModelDto, Patient>()
                .ForMember(dest=>dest.UserName, opt => opt.MapFrom(src => src.Email))
                .ReverseMap();

            //CreateMap<DoctorModelDto, ApplicationUser>().ReverseM;ap();
            CreateMap<DiscountCodeDto, DiscountCode>()
                .ReverseMap();
            CreateMap<AccountModelDto, ApplicationUser>().ReverseMap();
                //.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                //.ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Code))
                //.ForMember(dest => dest.DiscountType, opt => opt.MapFrom(src => src.DiscountType))
                //.ForMember(dest => dest.DiscountValue, opt => opt.MapFrom(src => src.DiscountValue))
                //.ForMember(dest => dest.NumberOfRequiredBookings, opt => opt.MapFrom(src => src.NumberOfRequiredBookings))
        }

    }
}


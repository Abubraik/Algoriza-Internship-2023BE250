using AutoMapper;
using Vezeeta.Core.Models;
using Vezeeta.Core.Models.Users;
using Vezeeta.Services.Models.DTOs;
using Vezeeta.Sevices.Models.DTOs;

namespace Vezeeta.Sevices.Helpers
{
    public class Mappers : Profile
    {
        public Mappers()
        {
            CreateMap<AccountModelDto, ApplicationUser>().ReverseMap();
            CreateMap<CreateDoctorModelDto, ApplicationUser>().ReverseMap();
            CreateMap<CreateDoctorModelDto, Doctor>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Specialization, opt => opt.Ignore())
                 .AfterMap((src, dest, context) =>
                 {
                     dest.Photo = (string)context.Items["Photo"];
                 })
                .ReverseMap();
            CreateMap<AccountModelDto, Patient>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
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
            CreateMap<Patient, PatientModelDto>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Photo))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName}  {src.LastName}"))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth))
                .ForMember(dest => dest.Bookings, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<Doctor, DoctorInfoDto>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Photo))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName}  {src.LastName}"))
                .ForMember(dest => dest.Specialize, opt => opt.MapFrom(src => src.Specialization))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
                .ReverseMap();
            CreateMap<DiscountCodeDto, DiscountCode>().ReverseMap();
        }

    }
}


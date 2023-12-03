using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Core.Models.Users;

namespace Vezeeta.Sevices.Helpers
{
    public class Mappers :Profile
    {
        public Mappers()
        {
            CreateMap<Doctor, DoctorModel>().ReverseMap();
            CreateMap<ApplicationUser, DoctorModel>().ReverseMap();
            CreateMap<AccountModel, ApplicationUser>().ReverseMap();
            CreateMap<DoctorModel, ApplicationUser>().ReverseMap();

        }
    }
}

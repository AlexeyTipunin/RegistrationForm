using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using RegistrationForm.Models;
using Entities = RegistrationForm.DAL.src.Entities;

namespace RegistrationForm.Infrastructure.AutoMapper
{
    public class ModelsProfile: Profile
    {
        public ModelsProfile()
        {
            CreateMap<Entities.Country, Country>();
            CreateMap<Entities.Province, Province>();
        }
    }
}

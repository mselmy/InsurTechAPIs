﻿using AutoMapper;
using InsurTech.APIs.DTOs.HealthInsurancePlanDTO;
using InsurTech.APIs.DTOs.HomeInsurancePlanDTO;
using InsurTech.APIs.DTOs.MotorInsurancePlanDTO;
using InsurTech.Core.Entities;

namespace InsurTech.APIs.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<HealthInsurancePlan, HealthInsuranceDTO>()
                 .ForMember(dest => dest.NumberOfUsers, opt => opt.MapFrom(src => src.Requests.Count()))
                 .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.Company.UserName ?? "no company"))
                 .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name));

            CreateMap<HomeInsurancePlan, HomeInsuranceDTO>()
                .ForMember(dest => dest.NumberOfUsers, opt => opt.MapFrom(src => src.Requests.Count()))
                .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.Company.UserName ?? "no company"))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name));

            CreateMap<MotorInsurancePlan, MotorInsuranceDTO>()
                .ForMember(dest => dest.NumberOfUsers, opt => opt.MapFrom(src => src.Requests.Count()))
                .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.Company.UserName ?? "no company"))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name));
        }
    }
}
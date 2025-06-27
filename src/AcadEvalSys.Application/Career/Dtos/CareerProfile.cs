using AcadEvalSys.Application.Career.Commands.CreateCareer;
using AcadEvalSys.Application.Career.Commands.UpdateCareer;
using AcadEvalSys.Domain.Entities;
using AutoMapper;

namespace AcadEvalSys.Application.Career.Dtos;

public class CareerProfile : Profile
{
    public CareerProfile()
    {
        CreateMap<TechnicalCareer, CareerDto>();
        CreateMap<CreateCareerCommand, TechnicalCareer>();
        CreateMap<UpdateCareerCommand, TechnicalCareer>();
    }
}
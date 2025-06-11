using AcadEvalSys.Domain.Entities;
using AutoMapper;

namespace AcadEvalSys.Application.Career.Dtos;

public class CareerProfile : Profile
{
    public CareerProfile()
    {
        CreateMap<TechnicalCareer, CareerDto>();
    }
}
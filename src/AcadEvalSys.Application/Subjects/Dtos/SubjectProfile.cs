using AcadEvalSys.Domain.Entities;
using AcadEvalSys.Application.Students.Dtos;
using AutoMapper;

namespace AcadEvalSys.Application.Subjects.Dtos;

public class SubjectProfile : Profile
{
    public SubjectProfile()
    {
        CreateMap<Subject, SubjectDto>()
            .ForMember(dest => dest.TechnicalCareerName, opt => opt.MapFrom(src => src.TechnicalCareer!.Name))
            .ForMember(dest => dest.ProfessorName, opt => opt.MapFrom(src => src.Professor!.User!.Name))
            .ForMember(dest => dest.Students, opt => opt.MapFrom(src => src.StudentSubjects!.Where(ss => ss.Student != null).Select(ss => ss.Student!)));
    }
}
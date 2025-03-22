using AutoMapper;
using OnlineTestForCheckingKnowledge.Business.DTOs;
using OnlineTestForCheckingKnowledge.Data.Entities;

namespace OnlineTestForCheckingKnowledge.Business.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Test, TestDto>().ReverseMap();
            CreateMap<Question, QuestionDto>().ReverseMap();
        }
    }
}
using AutoMapper;
using InsurTech.APIs.DTOs.FeedbackDTOs;
using InsurTech.Core.Entities;

public class FeedbackMapProfile : Profile
{
    public FeedbackMapProfile()
    {
        CreateMap<Feedback, FeedbackDto>().ReverseMap();
        CreateMap<CreateFeedbackInput, Feedback>().ReverseMap();
    }
}

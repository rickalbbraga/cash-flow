using System.Diagnostics.CodeAnalysis;
using Application.Results;
using AutoMapper;
using Domain.Entities;

namespace Application.Configurations
{
    [ExcludeFromCodeCoverage]
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            CreateMap<Entry, EntryResponse>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(d => d.Date, opt => opt.MapFrom(s => s.Date))
                .ForMember(d => d.Description, opt => opt.MapFrom(s => s.Description))
                .ForMember(d => d.Value, opt => opt.MapFrom(s => s.Value))
                .ForMember(d => d.Type, opt => opt.MapFrom(s => s.Type));
        }
    }
}
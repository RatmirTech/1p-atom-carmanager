using _1p_atom_carmanager.backend.core.Entities;
using _1p_atom_carmanager.backend.core.Requests;
using AutoMapper;

namespace _1p_atom_carmanager.backend.core.Mapping;
public class AppMappingProfile : Profile
{
    /// <summary>
    /// Профайл для маппинга запросов и сущностей
    /// </summary>
    public AppMappingProfile()
    {
        CreateMap<RegNewCarRequest, Car>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
            .ForMember(dest => dest.TachographInformation, opt => opt.Ignore())
            .ForMember(dest => dest.CarType, opt => opt.Ignore());
        CreateMap<Car, RegNewCarRequest>().ForAllMembers(opt => opt.Ignore());
        CreateMap<UpdateCartInfoByLicensePlate, Car>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
            .ForMember(dest => dest.TachographInformation, opt => opt.Ignore())
            .ForMember(dest => dest.CarType, opt => opt.Ignore()); ;
        CreateMap<RegCarTachographInfo, TachographInfo>().ReverseMap();
    }
}

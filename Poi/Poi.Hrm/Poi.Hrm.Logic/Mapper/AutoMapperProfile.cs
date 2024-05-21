using AutoMapper;
using Poi.Hrm.Logic.Requests;
using Poi.Id.InfraModel.DataAccess;
public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<CreateHoSoNhanSuRequest, HoSoNhanSu>()
            .ForAllMembers(opts =>
            {
                opts.Condition((src, dest, srcMember) => srcMember != null);
            });
    }
}
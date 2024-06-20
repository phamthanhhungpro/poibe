using AutoMapper;
using Poi.Prj.Logic.Requests;
using Poi.Id.InfraModel.DataAccess.Hrm;
public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        //CreateMap<CreateHoSoNhanSuRequest, HrmHoSoNhanSu>()
        //    .ForAllMembers(opts =>
        //    {
        //        opts.Condition((src, dest, srcMember) => srcMember != null);
        //    });
    }
}
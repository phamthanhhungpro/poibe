using AutoMapper;
using Poi.Id.InfraModel.DataAccess;
using Poi.Id.Logic.Requests;
public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<CreateTenantRequest, Tenant>()
                          .ForAllMembers(opts =>
                          {
                              opts.Condition((src, dest, srcMember) => srcMember != null);
                          });
    }
}
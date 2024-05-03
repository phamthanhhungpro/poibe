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

        CreateMap<AppRequest, App>()
                          .ForAllMembers(opts =>
                          {
                              opts.Condition((src, dest, srcMember) => srcMember != null);
                          });

        CreateMap<GroupRequest, Group>()
                          .ForAllMembers(opts =>
                          {
                              opts.Condition((src, dest, srcMember) => srcMember != null);
                          });

        CreateMap<RoleRequest, Role>()
                          .ForAllMembers(opts =>
                          {
                              opts.Condition((src, dest, srcMember) => srcMember != null);
                          });
    }
}
namespace Example.Web.Areas.Default
{
    using AutoMapper;

    using Example.Services;
    using Example.Web.Areas.Default.Models;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Search
            CreateMap<DashboardIndexForm, DataSearchParameter>()
                .ForMember(d => d.Page, opt => opt.MapFrom(s => s.Page ?? 1));
        }
    }
}

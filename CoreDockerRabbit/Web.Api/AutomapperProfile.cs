using AutoMapper;
using Web.Contracts;
using Web.DAL.Entities;

namespace Web.Api
{
    public class AutomapperProfile: Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Process, ProcessModel>();
            CreateMap<ProcessModel, Process>();
        }
    }
}

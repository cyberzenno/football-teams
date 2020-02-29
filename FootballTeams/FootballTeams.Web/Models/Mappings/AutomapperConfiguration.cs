using AutoMapper;

namespace FootballTeams.Web.Models.Mappings
{
    public class AutoMapperConfiguration
    {
        [System.Obsolete]
        public static void Configure()
        {
            Mapper.Initialize(x =>
            {
                x.AddProfile<MappingProfile>();
            });
        }
    }
}

namespace Tax.UnitTests.Setup
{
    using AutoMapper;
    using Tax.Services.Mapper;

    /// <summary>
    /// The AutoMapperSetup class.
    /// </summary>
    public class AutoMapperSetup
    {

        /// <summary>
        /// Creates an Automapper configuration for use in testing
        /// </summary>
        /// <returns></returns>
        public IMapper CreateAutoMapperConfig() {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MapperProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            return mapper;
        }
    }
}

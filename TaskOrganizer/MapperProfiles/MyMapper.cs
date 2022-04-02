using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskOrganizer.MapperProfiles
{
    /// <summary>
    /// Initialization of automapper configuration
    /// </summary>
    public static class MyMapper
    {
        private static bool isInitialized { get; set; }
        /// <summary>
        /// Method responsible for configuration and collection of profiles
        /// </summary>
        /// <param name="mapper"></param>
        /// <returns>Configured mapper with specified profiles</returns>
        public static IMapper Initialize(this IMapper mapper)
        {
            if (!isInitialized)
            {
                var config = new MapperConfigurationExpression();
                config.AddProfile<TaskProfile>();
                var mapperConfig = new MapperConfiguration(config);
                mapper = new Mapper(mapperConfig);
                isInitialized = true;
            }
            return mapper;
        }
    }
}

using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskOrganizer.MapperProfiles
{
    public static class MyMapper
    {
        private static bool isInitialized { get; set; }
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

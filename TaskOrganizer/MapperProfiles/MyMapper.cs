using AutoMapper;

namespace TaskOrganizer.MapperProfiles;

/// <summary>
/// Initialization of automapper configuration
/// </summary>
public static class MyMapper
{
    private static bool IsInitialized { get; set; }
    /// <summary>
    /// Method responsible for configuration and collection of profiles
    /// </summary>
    /// <param name="mapper"></param>
    /// <returns>Configured mapper with specified profiles</returns>
    public static IMapper Initialize(this IMapper mapper)
    {
        if (!IsInitialized)
        {
            MapperConfigurationExpression config = new();
            MapperConfiguration mapperConfig = new(config);
            config.AddProfile<TaskProfile>();
            mapper = new Mapper(mapperConfig);
            IsInitialized = true;
        }
        return mapper;
    }
}

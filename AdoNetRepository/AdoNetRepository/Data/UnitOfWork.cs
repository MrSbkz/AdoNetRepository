using AdoNetRepository.Data.Repositories;

namespace AdoNetRepository.Data;

public class UnitOfWork
{
    public CountryRepository Countries { get; set; }

    public CityRepository Cities { get; set; }

    public RoleRepository Roles { get; set; }

    public UserRepository Users { get; set; }

    public UserRolesRepository UserRoles { get; set; }

    public UnitOfWork(
        CountryRepository countryRepository,
        CityRepository cityRepository,
        RoleRepository roleRepository,
        UserRepository userRepository,
        UserRolesRepository userRolesRepository)
    {
        Countries = countryRepository;
        Cities = cityRepository;
        Roles = roleRepository;
        Users = userRepository;
        UserRoles = userRolesRepository;
    }
}
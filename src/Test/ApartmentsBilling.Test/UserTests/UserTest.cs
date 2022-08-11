using ApartmentsBilling.BacGroundJobs.Features.Abstract;
using ApartmentsBilling.BussinessLayer.Features.Abstract.InterFaces;
using ApartmentsBilling.BussinessLayer.Features.Concrete.Repositories;
using ApartmentsBilling.BussinessLayer.Mapper;
using ApartmentsBilling.Common.Dtos.UserDtos;
using ApartmentsBilling.DataAccesLayer.Features.Abstract;
using ApartmentsBilling.Entity.Entities;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Moq;

namespace ApartmentsBilling.Test.UserTests
{
    public class UserTest
    {
        public void InsertTest()
        {
            var UserrepositoryMock = new Mock<IGenericRepository<User>>();
            UserrepositoryMock.Setup(x => x.AddAsync(It.IsAny<User>()));
            var FlatService = new Mock<IFlatService>();
            var jobsMock = new Mock<IJobs>();
            jobsMock.Setup(x => x.FireAndForget(It.IsAny<string>(), It.IsAny<string>()));
            var configurationMock = new Mock<IConfiguration>();
            configurationMock.Setup(x => x.GetSection(It.IsAny<string>()));

            MapperConfiguration mapperConfig = new(cfg =>
            {
                cfg.AddProfile(new GenericProfile());
            });

            IMapper _mapper = new Mapper(mapperConfig);
            _mapper.ConfigurationProvider.AssertConfigurationIsValid();

            var userService = new UserService(UserrepositoryMock.Object, _mapper, configurationMock.Object, jobsMock.Object, FlatService.Object);

            CreateUserDto user = new()
            {
                Email = "fatih@gmail.com",
                FullName = "Fatih SAĞLAM",

            };
        }
    }
}

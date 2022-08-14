using ApartmentsBilling.BacGroundJobs.Features.Abstract;
using ApartmentsBilling.BussinessLayer.Mapper;
using ApartmentsBilling.DataAccesLayer.Abstract;
using ApartmentsBilling.Entity.Entities.Common;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Moq;

namespace ApartmentsBilling.Test.UserTest
{
    public class BaseTest<T> where T : BaseEntity
    {

        protected readonly Mock<IGenericRepository<T>> repositoryMock = new();
        protected readonly Mock<IJobs> JobsMock = new();
        protected readonly Mock<IConfiguration> configurationMock = new();
        MapperConfiguration mapperConfig = new(cfg =>
        {
            cfg.AddProfile(new GenericProfile());
            cfg.AllowNullDestinationValues = true;
        });
        protected readonly IMapper _mapper;
        public BaseTest()
        {
            JobsMock.Setup(x => x.FireAndForget(It.IsAny<string>(), It.IsAny<string>()));
            configurationMock.Setup(x => x.GetSection(It.IsAny<string>()));
            _mapper = new Mapper(mapperConfig);
        }

    }


}

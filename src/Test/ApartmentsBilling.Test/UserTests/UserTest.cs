using ApartmentsBilling.Entity.Entities;
using ApartmentsBilling.Test.UserTest;

namespace ApartmentsBilling.Test.UserTests
{
    public class UserTest : BaseTest<User>
    {
        //private Mock<CreateUserDtoValidation> Uservalidate { get; set; }

        //public UserTest(Mock<CreateUserDtoValidation> uservalidate)
        //{
        //    Uservalidate = uservalidate;
        //}

        //[Fact]
        //public async Task InsertTest()
        //{

        //    var UserrepositoryMock = new Mock<IUserRepository>();
        //    UserrepositoryMock.Setup(x => x.AddAsync(It.IsAny<User>()));

        //    var FlatrepositoryMock = new Mock<IFlatRepository>();
        //    FlatrepositoryMock.Setup(f => f.GetSingleAsync(It.IsAny<Expression<Func<Flat, bool>>>())).Callback((Expression<Func<Flat, bool>>[] expressions) =>
        //    {
        //        if (expressions == null || expressions.Any() == false)
        //        {
        //            return;
        //        }
        //        Func<Flat, bool> wereLambdaExpression = expressions.First().Compile();  //  x=>x.isActive is here
        //    })
        //           .ReturnsAsync();

        //    var FlatService = new Mock<IFlatService>();
        //    var jobsMock = new Mock<IJobs>();
        //    jobsMock.Setup(x => x.FireAndForget(It.IsAny<string>(), It.IsAny<string>()));
        //    var configurationMock = new Mock<IConfiguration>();
        //    //object value = configurationMock.Setup(x => x.GetSection(It.IsAny<string>()));

        //    MapperConfiguration mapperConfig = new(cfg =>
        //    {
        //        cfg.AddProfile(new GenericProfile());
        //        cfg.AllowNullDestinationValues = true;
        //    });

        //    IMapper _mapper = new Mapper(mapperConfig);

        //    //var _invidualvalid = new Mock<CreateUserDtoValidation>().Setup(m=> m.Validate(It.IsAny<ValidationContext<CreateUserDtoValidation>>()));
        //    var userService = new UserService(_mapper, FlatrepositoryMock.Object, UserrepositoryMock.Object, JobsMock.Object);

        //    CreateUserDto user = new()
        //    {
        //        Email = "fatih@gmail.com",
        //        FullName = "Fatih SAĞLAM",
        //        FlatId = Guid.Parse("87242c92-41d2-4443-a5f9-08da7d985d16"),
        //        IdNumber = "11111111111",
        //        PhoneNumber = "5555555555",
        //        Role = UserRole.User
        //    };

        //    var response = await userService.AddUserAsync(user, false);
        //    response.Should().BeTrue();
        //}

    }
}

using ApartmentsBilling.DataAccesLayer.Features.Abstract;
using ApartmentsBilling.Entity.Entities.Common;
using Moq;

namespace ApartmentsBilling.Test.UserTest
{
    public class BaseTest<T> where T : BaseEntity
    {
        protected readonly Mock<IGenericRepository<T>> mock = new();
        protected readonly Mock<IGenericRepository<T>> mock2 = new();
    }
}

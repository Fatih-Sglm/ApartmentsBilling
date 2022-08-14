using ApartmentsBilling.BussinessLayer.Configuration.Validations.BillTypeValidation;
using ApartmentsBilling.BussinessLayer.Features.Concrete.Repositories;
using ApartmentsBilling.BussinessLayer.Mapper;
using ApartmentsBilling.Common.Dtos.BillTypeDto;
using ApartmentsBilling.DataAccesLayer.InterFaces;
using AutoMapper;
using FluentAssertions;
using FluentValidation.Results;
using Moq;
using System;
using Xunit;

namespace ApartmentsBilling.Test.UserTests
{
    public class BillTypeTest
    {


        [Fact]
        public async void InsertBillType()
        {
            var billtyperepository = new Mock<IBillTypeRepository>();

            MapperConfiguration mapperConfig = new(cfg =>
            {
                cfg.AddProfile(new GenericProfile());
                cfg.AllowNullDestinationValues = true;
            });

            IMapper _mapper = new Mapper(mapperConfig);

            var billtypeservice = new BillTypeService(billtyperepository.Object, _mapper);

            CreateBillTypeDto createBillTypeDto = new() { Name = "Doğal Gaz" };
            var response = await billtypeservice.AddAsync(createBillTypeDto);

            response.Should().BeTrue();
        }

        [Fact]
        public async void UpdateBillType()
        {
            var billtyperepository = new Mock<IBillTypeRepository>();
            MapperConfiguration mapperConfig = new(cfg =>
            {
                cfg.AddProfile(new GenericProfile());
                cfg.AllowNullDestinationValues = true;
            });

            IMapper _mapper = new Mapper(mapperConfig);
            var billtypeservice = new BillTypeService(billtyperepository.Object, _mapper);

            UpdateBillTypeDto updateBillTypeDto = new()
            {
                Id = Guid.Parse("02ea102d-ae4a-4c7d-ba9d-08da7de4dd6e"),
                Name = "Doğal Gaz"
            };
            var response = await billtypeservice.UpdateAsync(updateBillTypeDto);

            var validator = new UpdateBillTypeDtoValidation();
            ValidationResult result = validator.Validate(updateBillTypeDto);
            result.IsValid.Should().BeTrue();
            response.Should().BeTrue();
        }

        [Fact]
        public async void GetBillType()
        {
            var billtyperepository = new Mock<IBillTypeRepository>();
            //billtyperepository.Setup(x => x.GetSingleAsync(It.IsAny<Expression<Func<Flat, bool>>>()).ReturnAsync((Expression<Func<Flat, bool>> predicate) =>  ));
            MapperConfiguration mapperConfig = new(cfg =>
            {
                cfg.AddProfile(new GenericProfile());
                cfg.AllowNullDestinationValues = true;
            });

            IMapper _mapper = new Mapper(mapperConfig);
            var billtypeservice = new BillTypeService(billtyperepository.Object, _mapper);

            string id = "02ea102d-ae4a-4c7d-ba9d-08da7de4dd6e";


            var response = await billtypeservice.GetSingleAsync(x => x.Id == Guid.Parse(id));
        }
    }
}

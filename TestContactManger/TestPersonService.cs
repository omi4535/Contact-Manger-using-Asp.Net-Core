using Entity;
using ServiceContract.DTO.Person;
using services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace TestContactManger
{
    public class TestPersonService
    {
       
        private readonly PersonService _service;
        private readonly ITestOutputHelper _testOutputHelper;

        public TestPersonService(ITestOutputHelper test)
        {
            _service = new PersonService(false); // ✅ real object, no mock
            _testOutputHelper = test;
        }

        [Fact]
        public void AddPerson_ShouldReturnPersonRes_WhenValid()
        {
            var req = new PersonAddReq
            {
                FirstName = "John",
                Email = "john@test.com"
                //CountryId = Guid.NewGuid()
            };

            var result = _service.AddPerson(req);

            Assert.NotNull(result);
            Assert.Equal("John", result.FirstName);
        }

        [Fact]
        public void GetAllPerson_ShouldReturnEmptyList_WhenNoData()
        {
            var result = _service.GetAllPerson();

            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public void GetAllPerson_ShouldReturnList_WhenDataExists()
        {
            var req = new PersonAddReq
            {
                FirstName = "Alice",
                Email = "alice@test.com",
                CountryId = Guid.NewGuid()
            };

            _service.AddPerson(req);

            var result = _service.GetAllPerson();
            _testOutputHelper.WriteLine("sdfags");
            Assert.NotEmpty(result);
            Assert.Contains(result, r => r.FirstName == "Alice");
        }

        [Fact]
        public void GetPersonById_ShouldReturnCorrectPerson()
        {
            var req = new PersonAddReq
            {
                FirstName = "Bob",
                Email = "bob@test.com",
                CountryId = Guid.NewGuid()
            };

            var added = _service.AddPerson(req);

            var result = _service.GetPersonById(added.Id);

            Assert.NotNull(result);
            Assert.Equal("Bob", result.FirstName);
        }

        [Fact]
        public void GetPersonById_ShouldThrow_WhenIdIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => _service.GetPersonById(null));
        }

        [Fact]
        public void EditPerson_ShouldUpdatePerson_WhenValid()
        {
            var req = new PersonAddReq
            {
                FirstName = "Chris",
                Email = "chris@test.com",
                CountryId = Guid.NewGuid()
            };

            var added = _service.AddPerson(req);

            var editReq = new PersonEditReq
            {
                Id = added.Id,
                FirstName = "Chris Updated",
                Email = "chris.updated@test.com",
                CountryId = added.Id
            };

            var result = _service.EditPerson(editReq);

            Assert.Equal("Chris Updated", result.FirstName);
        }

        [Fact]
        public void DeletePerson_ShouldReturn1_WhenPersonExists()
        {
            var req = new PersonAddReq
            {
                FirstName = "Mark",
                Email = "mark@test.com",
                CountryId = Guid.NewGuid()
            };

            var added = _service.AddPerson(req);

            var entity = new Person
            {
                Id = added.Id,
                FirstName = added.FirstName,
                Email = added.Email,
                CountryId = added.Id
            };

            var result = _service.DeletePerson(entity);

            Assert.True(result);
        }
    }


}

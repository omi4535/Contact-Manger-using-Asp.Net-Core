 using Entity;
using EntityFrameworkMock;
using Microsoft.EntityFrameworkCore;
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
            var options = new DbContextOptionsBuilder<ContactMangerDBContext>()
      .UseInMemoryDatabase(databaseName: "TestDb")
      .Options;
            var dbContext = new ContactMangerDBContext(options);
            
            _service = new PersonService(dbContext); // ✅ real object, no mock
        
            _testOutputHelper = test;
        }


        [Fact]
        public async Task AddPerson_ShouldReturnPersonRes_WhenValid()
        {
            var req = new PersonAddReq
            {
                FirstName = "John",
                LastName="dou",
                Email = "john@test.com"
                //CountryId = Guid.NewGuid()
            };

            PersonRes? result = await _service.AddPersonAsync(req);

            Assert.NotNull(result);
            Assert.Equal("John", result.FirstName);
        }

        [Fact]
        public async Task GetAllPerson_ShouldReturnList_WhenDataExists()
        {
            var req = new PersonAddReq
            {
                FirstName = "Alice",
                LastName = "Dhage",
                Email = "alice@test.com",
                CountryId = Guid.NewGuid()
            };

            await _service.AddPersonAsync(req);

            var result = await _service.GetAllPersonAsync();
            _testOutputHelper.WriteLine("sdfags");
            Assert.NotEmpty(result);
            Assert.Contains(result, r => r.FirstName == "Alice");
        }

        [Fact]
        public async Task GetPersonById_ShouldReturnCorrectPerson()
        {
            var req = new PersonAddReq
            {
                FirstName = "Bob",
                LastName = "Dhage",
                Email = "bob@test.com",
                CountryId = Guid.NewGuid()
            };

            var added = await _service.AddPersonAsync(req);

            var result = await _service.GetPersonByIdAsync(added.Id);

            Assert.NotNull(result);
            Assert.Equal("Bob", result.FirstName);
        }

        [Fact]
        public async Task GetPersonById_ShouldThrow_WhenIdIsNull()
        {
            Assert.ThrowsAsync<ArgumentNullException>( () =>  _service.GetPersonByIdAsync(null));
        }

        [Fact]
        public async Task EditPerson_ShouldUpdatePerson_WhenValid()
        {
            var req = new PersonAddReq
            {
                FirstName = "Chris",
                LastName="Dhage",
                Email = "chris@test.com",
                CountryId = Guid.NewGuid()
            };

            var added = await _service.AddPersonAsync(req);

            var editReq = new PersonEditReq
            {
                Id = added.Id,
                FirstName = "Chris Updated",
                LastName = "Dhage",
                Email = "chris.updated@test.com",
                CountryId = added.Id
            };

            var result = await _service.EditPersonAsync(editReq);

            Assert.Equal("Chris Updated", result.FirstName);
        }

        [Fact]
        public async Task DeletePerson_ShouldReturn1_WhenPersonExists()
        {
            var req = new PersonAddReq
            {
                FirstName = "Mark",
                LastName = "dhage",
                Email = "mark@test.com",
                CountryId = Guid.NewGuid()
            };

            var added = await _service.AddPersonAsync(req);

            Person entity =  new Person
            {
                Id = added.Id,
                FirstName = added.FirstName,
                Email = added.Email,
                CountryId = added.Id
            };

            var result = await _service.DeletePersonAsync(entity.Id);

            Assert.True(result);
        }
    }


}

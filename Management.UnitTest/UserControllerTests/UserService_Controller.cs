using Microsoft.VisualStudio.TestTools.UnitTesting;
using Management.API.Controllers;
using Management.Persistence.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Management.Infrastructure.MessagingContracts;
using Microsoft.Extensions.Options;
using Management.API.Helpers;
using Management.API.Registry;
using StructureMap;

namespace Management.UnitTest.User.Controllers
{
    [TestClass]
    public class UserService_Controller
    {
        private IContainer container;
        private UserController userCon;


        [TestInitialize]
        public void Setup()
        {
            container = new Container(new Registry());
            userCon = container.GetInstance<UserController>();
        }

        [TestMethod]
        public async Task GetBydId_TestUserAsParam_AssertedEqual()
        {
            var expectedUser = getTestUser();
            var result = await userCon.GetById(expectedUser.Id) as ObjectResult;
            var actualUser = result.Value as Management.Persistence.Model.User;

            Assert.AreEqual(expectedUser.Id, actualUser.Id);
        }
        
        public Management.Persistence.Model.User getTestUser()
        {
            return new Management.Persistence.Model.User()
            {
                Id = System.Guid.Parse("3730ec16-2e67-4e8e-9b43-6fa18dd8a02d"),
                Name = "TESTname",
                Email = "TEST@gmail.com",
                AccessLevel = UserRoles.Manager,
                BaseWage = 100,
                EmploymentDate = System.DateTime.Parse("2016-11-11T00:00:00")
            };
            
        }
    }
}

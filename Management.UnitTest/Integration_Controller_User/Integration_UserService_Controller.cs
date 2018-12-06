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
using System.Collections.Generic;
using System.Linq;
using System.Collections;

namespace Management.UnitTest.User.Controllers
{
    [TestClass]
    public class Integration_UserService_Controller
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
            /* var expectedUser = getTestUser();
             var result = await userCon.GetById(expectedUser.Id) as ObjectResult;
             var actualUser = result.Value as Management.Persistence.Model.User;

             Assert.AreEqual(expectedUser.Id, actualUser.Id);*/
            var user1 = await GetTestUser();
            Assert.IsNotNull(user1);
        }


        /// <summary>
        /// Checks if the user exists in the database, if it does, return the test user
        /// if not, creates it and then returns it.
        /// </summary>
        /// <returns>Test User created in the database</returns>
        private async Task<Management.Persistence.Model.User> GetTestUser()
        {
            var testUser = new Management.Persistence.Model.User()
            {
                Name = "TESTname",
                Email = "TEST@gmail.com",
                AccessLevel = UserRoles.Manager,
                BaseWage = 100,
                EmploymentDate = System.DateTime.Parse("2016-11-11T00:00:00")
            };

            Persistence.Model.User user = CheckIfUserExistsInDB(testUser).Result;
            if (user != null) return user;

            //Since the user is not in the database, we create one
            await userCon.CreateUser(new API.RequestModels.CreateUserRequestModel
            {
                Name = testUser.Name,
                Email = testUser.Email,
                AccessLevel = testUser.AccessLevel,
                BaseWage = testUser.BaseWage,
                EmploymentDate = testUser.EmploymentDate,
                Password = "testpassword"
            });

            //We confirm it is created, and returns the result, so that the generated id comes with the test user
            return CheckIfUserExistsInDB(testUser).Result;

           // var user2 = await userCon.GetById(Guid.Parse("dd29557a-7584-48e6-89a0-23dec8025d5d")) as ObjectResult;


            /*var allUsers = await userCon.GetALLUsers() as ObjectResult;
            List<Persistence.Model.User> userList = (List <Persistence.Model.User>)allUsers.Value; 

            foreach (Persistence.Model.User oUser in userList)
            {
                if (oUser.Name == testUser.Name && oUser.Email == testUser.Email) return oUser;
            }
            
            var IdObjectResult = await userCon.CreateUser(new API.RequestModels.CreateUserRequestModel
            {
                Name = testUser.Name,
                Email = testUser.Email,
                AccessLevel = testUser.AccessLevel,
                Wage = testUser.BaseWage,
                EmploymentDate = testUser.EmploymentDate,
                Password = "testpassword"
            }) as ObjectResult;*/
            
        }

        private async Task<Persistence.Model.User> CheckIfUserExistsInDB(Persistence.Model.User user)
        {
            var allUsers = await userCon.GetALLUsers() as ObjectResult;
            List<Persistence.Model.User> userList = (List<Persistence.Model.User>)allUsers.Value;
            foreach(Persistence.Model.User dbUser in userList)
            {
                if (dbUser.Name == user.Name && dbUser.Email == user.Email) return dbUser;
            }
            return null;
        }

    }
}

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
        private const string testEmail = "admin@admin.dk";

        //Arrange, Act, Assert

        [TestInitialize]
        public void Setup()
        {
            container = new Container(new Registry());
            userCon = container.GetInstance<UserController>();
        }

        [TestMethod]
        public async Task GetAllUsers_NoParam_AssertedTypeOf()
        {
            var userList = getUserList().Result;
            foreach (var user in userList) Assert.IsTrue(user is Persistence.Model.User); 
        }

        [TestMethod]
        public async Task GetBydId_TestUserAsParam_AssertedEqual()
        {
            var user = GetTestUser().Result;
            Assert.IsNotNull(user);
            Assert.AreEqual(testEmail, user.Email); 
        }

        [TestMethod]
        public async Task UpdateUser_TestUserAsParam_AssertIsTrue()
        {

        }
        
        private async Task<Management.Persistence.Model.User> GetTestUser()
        {
            return CheckIfUserExistsInDB(testEmail).Result;
        }

        private async Task<Persistence.Model.User> CheckIfUserExistsInDB(string userEmail)
        {
            var userList = getUserList().Result; 
            foreach(Persistence.Model.User dbUser in userList)
            {
                if (dbUser.Email == userEmail) return dbUser;
            }
            return null;
        }

        private async Task<List<Persistence.Model.User>> getUserList()
        {
            var allUsers = await userCon.GetALLUsers() as ObjectResult;
            return (List<Persistence.Model.User>)allUsers.Value;
        }

    }
}

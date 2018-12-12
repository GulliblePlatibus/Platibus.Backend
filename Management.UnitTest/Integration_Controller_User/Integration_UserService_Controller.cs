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
        private const string testEmail = "employee@employee.dk";
        private Persistence.Model.User user;

        //Arrange, Act, Assert

        [TestInitialize]
        public void Setup()
        {
            container = new Container(new Registry());
            userCon = container.GetInstance<UserController>();
            user = GetTestUser().Result; 
        }
        [TestMethod]
        public async Task Get_ShiftsReturnSuccessAndCorrectContentType()
        {
            var result = userCon.GetShiftsForUserWithIdAsync(user.Id).Result as ObjectResult;
            var value = result.Value as IEnumerable<Shift>;
            List<Shift> list = value.ToList();
            Assert.IsNotNull(list[0]); 
        }
        
        [TestMethod]
        public async Task Get_UserListReturnSuccessAndCorrectContentType()
        {
            var result = userCon.GetALLUsers().Result as ObjectResult;
            var userlist = (List<Persistence.Model.User>)result.Value;

            Assert.IsTrue(userlist is List<Persistence.Model.User>); 
        }

        [TestMethod]
        public async Task Get_UserReturnSuccess()
        {
            var user = GetTestUser().Result;
            Assert.IsNotNull(user);
            Assert.IsTrue(user is Persistence.Model.User); 
        }

        [TestMethod]
        public async Task Get_UserCorrectContentType()
        {
            var user = GetTestUser().Result;
            Assert.AreEqual(testEmail, user.Email);
        }

        [TestMethod]
        public async Task Post_UpdateSuccess()
        {
            var newName = "TEST";
            var result = await userCon.UpdateUserByIdObjectAsParam(user.Id, new API.RequestModels.UpdateUserRequestModel
            {
                Name = newName
            });
            
            var updatedUser = (Persistence.Model.User)userCon.GetById(user.Id).Result;
            var updatedName = updatedUser.Name;

            //Reset test user
            var result2 = await userCon.UpdateUserByIdObjectAsParam(user.Id, new API.RequestModels.UpdateUserRequestModel
            {
                Name = "admin"
            });

            Assert.AreEqual(newName, updatedName); 

        }

        [TestMethod]
        public async Task Get_SalaryReturnSuccessCorrectContentType()
        {
            //Arrange
            var result = userCon.GetSalaryForUserWithIdAsync(user.Id, DateTime.Now.AddYears(-10).Ticks, DateTime.Now.Ticks).Result as ObjectResult;

            //Act
            var ShiftPaymentList = (List<Domain.DomainElements.BudgetPlanner.ShiftPayment>)result.Value;
            
            //Assert
            Assert.IsTrue(ShiftPaymentList is List<Domain.DomainElements.BudgetPlanner.ShiftPayment>);
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

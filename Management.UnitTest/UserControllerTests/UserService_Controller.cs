using Microsoft.VisualStudio.TestTools.UnitTesting;
using Management.API.Controllers;
using Management.Persistence.Model;

namespace Management.UnitTest.User.Controllers
{
    [TestClass]
    public class UserService_Controller
    {
        private UserController _userController;

        public UserService_Controller(UserController userController)
        {
            _userController = userController;
        }

        [TestMethod]
        public async void GetBydId_OptionalValuesFromGet_AssertedEqual()
        {
            var expectedUser = getTestUser();
            await _userController.CreateUser(new API.RequestModels.CreateUserRequestModel()
            {
                Name = expectedUser.Name, 
                Email = expectedUser.Email, 
                Password = "test", 
                Acceslevel = expectedUser.AccessLevel, 
               // Wage = expectedUser.BaseWage 
            }); 

           // var gottenUser = _userController.GetById(); 
        }

        public Management.Persistence.Model.User getTestUser()
        {
            var testUser = new Management.Persistence.Model.User()
            {
                Name = "TestName",
                Email = "Test@Test.com",
                AccessLevel = 2,
                BaseWage = 100,
                EmploymentDate = System.DateTime.Parse("20-08-1994")
            };
            return testUser;
        }
    }
}

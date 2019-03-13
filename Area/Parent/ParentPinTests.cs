using Maksim.Web.SeleniumTests.Pages;
using NUnit.Framework;

namespace Maksim.Web.SeleniumTests.Area.Parent
{
    [TestFixture]
    class ParentPinTests : BaseTest
    {
        private BaseTest _baseTest;

        [SetUp]
        public void SetUp()
        {
            _baseTest = new BaseTest();
        }

        [TearDown]
        public void Cleanup()
        {
            _baseTest.CleanUp();
        }

        [Test]
        public void CreatePinByParent_CorrectData_SucessResult()
        {
            //Login to a front page as a Parent
           var loginPage =  new Login(driver, Users.Parent1);

            //Login to Pin edit page as a parent
            var Pin = new Pin(driver, Users.Parent1);

            //Run Create pin method
            Pin.CreatePinByParent();

            //Run Delete pin method
            Pin.DeleteCreatedPinByParent();

            //Run method logout
            loginPage.Logout();
        }


        [Test]
        public void EditCreatedPinByParent_CorrectData_SucessResult()
        {

            //Login to a front page as a Parent
            var loginPage = new Login(driver, Users.Parent1);

            //Login to Pin edit page as a parent
            var Pin = new Pin(driver, Users.Parent1);

            //Run Create pin method
            Pin.CreatePinByParent();

            //Run Edit pin method
            Pin.EditPinByParent();

            //Run Delete pin method
            Pin.DeleteEditedPinByParent();

            //Run method logout
            loginPage.Logout();
        }

        [Test]
        public void DeleteCreatedByParentPin_CorrectData_SucessResult()

        {

            //Login to a front page as a Parent
            var loginPage = new Login(driver, Users.Parent1);

            //Login to Pin edit page as a parent
            var Pin = new Pin(driver, Users.Parent1);

            //Run Delete pin method
            Pin.DeletePinByParent();

            //Run method logout
            loginPage.Logout();
        }

    }
}


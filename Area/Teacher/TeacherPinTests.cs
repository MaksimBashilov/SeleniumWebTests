using Maksim.Web.SeleniumTests.Pages;
using NUnit.Framework;


namespace Maksim.Web.SeleniumTests.Area.Teacher
{

    [TestFixture]
    public class TeacherPinTests : BaseTest
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
        public void CreatePinByTeacher_CorrectData_SucessResult()
        {
            // Login as a teacher
            var loginPage = new Login(driver, Users.Teacher1);

            //Login to Pin edit page as a parent
            var Pin = new Pin(driver, Users.Teacher1);

            //Run Create pin method
            Pin.CreatePinByTeacher();

            //Run Create pin method
            Pin.DeleteCreatedPinByTeacher();

            //Run method logout
            loginPage.Logout();
        }

        [Test]
        public void CreatePinByTeacher_PinNotCreated_FailResult()
        {
            // Login as a teacher
            var loginPage = new Login(driver, Users.Teacher1);

            //Login to Pin edit page as a parent
            var Pin = new Pin(driver, Users.Teacher1);

            //Run Create pin method
            Pin.TeacherNotCreatePin();

            //Run method logout
            loginPage.Logout();
        }


        [Test]
        public void EditCreatedPinByTeacher_CorrectData_SucessResult()
        {

            // Login as a teacher
            var loginPage = new Login(driver, Users.Teacher1);

            //Login to Pin edit page as a parent
            var Pin = new Pin(driver, Users.Teacher1);

            //Run Create pin method
            Pin.CreatePinByTeacher();

            //Run Create pin method
            Pin.EditPinByTeacher();

            //Run Create pin method
            Pin.DeleteEditedPinByTeacher();

            //Run method logout
            loginPage.Logout();
        }

        [Test]

        public void DeleteCreatedByTeacherPin_CorrectData_SucessResult()

        {

            // Login as a teacher
            var loginPage = new Login(driver, Users.Teacher1);

            //Login to Pin edit page as a parent
            var Pin = new Pin(driver, Users.Teacher1);

            //Run Create pin method
            Pin.DeletePinByTeacher();

            //Run method logout
            loginPage.Logout();

        }

    }
}
using Maksim.Web.SeleniumTests.Pages;
using NUnit.Framework;


namespace Maksim.Web.SeleniumTests.Area.Configuration
{
    [TestFixture]
    public class LoginTests : BaseTest
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
        public void LoginAsAParent_WrongLogin_FailResult()
        {
            var loginPage = new Login(driver, Users.Parent2);

            loginPage.WrongPasswordParent();

        }


    }
}

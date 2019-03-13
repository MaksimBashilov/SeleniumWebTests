using Maksim.Web.SeleniumTests.Pages;
using NUnit.Framework;

namespace Maksim.Web.SeleniumTests.Area.Parent
{
    [TestFixture]
    class ParentContactBookTests : BaseTest
    {
        private BaseTest _baseTest;

        [SetUp]
        public void SetUp()
        {
            _baseTest = new BaseTest();
        }

        [Test]
        public void CreateContactBookByParent_CorrectData_SuccessResult()
        {
            //Login to a front page as a Parent
            var loginPage = new Login(driver, Users.Parent1);

            //Open Contact book page
            var ContactBook = new ContactBook(driver, Users.Parent1);

            //Create note
            ContactBook.ContactBookNoteParent();
        
            //Run method logout
            loginPage.Logout();

        }

        [Test]
        public void CreateContactBookReplyByParent_CorrectData_SuccessResult()
        {
            //Login to a front page as a Parent
            var loginPage = new Login(driver, Users.Parent1);

            //Open Contact book page
            var ContactBook = new ContactBook(driver, Users.Parent1);

            //Create note
            ContactBook.ContactBookNoteParent();

            //Reply Note 
            ContactBook.ContactBookNoteCommentParent();

            //Run method logout
            loginPage.Logout();
        }

        [TearDown]
        public void Cleanup()
        {
            _baseTest.CleanUp();
        }
    }
}

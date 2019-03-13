using Maksim.Web.SeleniumTests.Pages;
using NUnit.Framework;

namespace Maksim.Web.SeleniumTests.Area.Teacher
{
    [TestFixture]
    class TeacherContactBookTests : BaseTest
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

        public void CreateContactBookByTeacher_CorrectData_SuccessResult()
        {
            // Login as a teacher
            var loginPage = new Login(driver, Users.Teacher1);

            //Open Contact book page
            var ContactBook = new ContactBook(driver, Users.Teacher1);

            //Create note
            ContactBook.ContactBookNoteTeacher();

            //Run method logout
            loginPage.Logout();
        }


        [Test]

        public void CreateContactBookReplyByTeacher_CorrectData_SuccessResult()
        {
            // Login as a teacher
            var loginPage = new Login(driver, Users.Teacher1);

            //Open Contact book page
            var ContactBook = new ContactBook(driver, Users.Teacher1);

            //Create note
            ContactBook.ContactBookNoteTeacher();

            //Reply Note 
            ContactBook.ContactBookNoteCommentTeacher();

            //Run method logout
            loginPage.Logout();
        }

        [Test]

        public void CreateContactBookByTeacher_CancelCreation_FailResult()
        {
            // Login as a teacher
            var loginPage = new Login(driver, Users.Teacher1);

            //Open Contact book page
            var ContactBook = new ContactBook(driver, Users.Teacher1);

            ContactBook.ContactBookNoteTeacherCancel(); 

            //Run method logout
            loginPage.Logout();
        }

    }
}
using Maksim.Web.SeleniumTests.Pages;
using NUnit.Framework;


namespace Maksim.Web.SeleniumTests.Area.Student
{
    [TestFixture]
    class StudentDocumentTests : BaseTest
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

        public void UploadDocumentByStudent_CorrectData_SucessResult()
        {
            // Login as a student
            var loginPage = new Login(driver, Users.Student);

            //Open doc page
            var Documents = new Documents(driver, Users.Student);

            //Create doc
            Documents.UploadDocumentByStudent();

            //Delete doc
            Documents.DeleteDocumentByStudent();

            //Run method logout
            loginPage.Logout();
        }

        [Test]
        public void EditDocumentByStudent_CorrectData_SucessResult()
        {
            // Login as a student
            var loginPage = new Login(driver, Users.Student);

            //Open doc page
            var Documents = new Documents(driver, Users.Student);

            //Create doc
            Documents.UploadDocumentByStudent();

            //Edit doc
            Documents.EditDocumentByStudent();

            //Delete doc
            Documents.DeleteEditedDocumentByStudent();

            //Run method logout
            loginPage.Logout();

        }

        [Test]
        public void DeleteDocumentByStudent_CorrectData_SucessResult()
        {
            // Login as a student
            var loginPage = new Login(driver, Users.Student);

            //Open doc page
            var Documents = new Documents(driver, Users.Student);

            //Create doc
            Documents.UploadDocumentByStudent();

            //Delete doc
            Documents.DeleteDocumentByStudent();

            //Run method logout
            loginPage.Logout();

        }
    }
}

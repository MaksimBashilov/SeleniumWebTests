using Maksim.Web.SeleniumTests.Pages;
using NUnit.Framework;


namespace Maksim.Web.SeleniumTests.Area.Teacher
{
    [TestFixture]
    class TeacherDocumentsTests : BaseTest
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
        public void UploadDocumentByTeacher_CorrectData_SucessResult()
        {
            // Login as a teacher
          var loginPage =  new Login(driver, Users.Teacher);

            //Open doc page
            var Documents = new Documents(driver, Users.Teacher);

            //Create doc
            Documents.UploadDocumentByTeacher();

            //Delete doc
            Documents.DeleteDocumentByTeacher();

            //Run method logout
            loginPage.Logout();

        }

        [Test]
        public void EditDocumentByTeacher_CorrectData_SucessResult()
        {
            // Login as a teacher
            var loginPage = new Login(driver, Users.Teacher1);

            //Open doc page
           var Documents = new Documents(driver, Users.Teacher1);

            //Create doc
            Documents.UploadDocumentByTeacher();

           //Edit doc
           Documents.EditDocumentByTeacher();

            //Delete doc
            Documents.DeleteEditedDocumentByTeacher();

            //Run method logout
            loginPage.Logout();

        }

        [Test]
        public void DeleteDocumentByTeacher_CorrectData_SucessResult()
        {
            // Login as a teacher
            var loginPage = new Login(driver, Users.Teacher1);

            //Open doc page
            var Documents = new Documents(driver, Users.Teacher1);

            //Create doc
            Documents.UploadDocumentByTeacher();

            //Delete doc
            Documents.DeleteDocumentByTeacher();

            //Run method logout
            loginPage.Logout();

        }

        [Test]
        public void EditDocumentWithWrongNamesTeacher_WrongData_NegativeResult()
        {
            // Login as a teacher
            var loginPage = new Login(driver, Users.Teacher1);

            //Open doc page
            var Documents = new Documents(driver, Users.Teacher1);

            //Create doc
            Documents.UploadDocumentByTeacher();

            //Check wrong names
            Documents.EditDocumentNameWithErrorsByTeacher();

            //Delete doc
            Documents.DeleteDocumentByTeacher();

            //Run method logout
            loginPage.Logout();

        }
    }
}
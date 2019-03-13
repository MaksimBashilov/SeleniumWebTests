using Maksim.Web.SeleniumTests.Pages;
using NUnit.Framework;

namespace Maksim.Web.SeleniumTests.Area.Parent
{
    [TestFixture]
    class ParentDocumentsTests : BaseTest
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

        public void UploadDocumentByParent_CorrectData_SucessResult()
        {
            // Login as a parent
            var loginPage = new Login(driver, Users.Parent1);

            //Open doc page
            var Documents = new Documents(driver, Users.Parent1);

            //Create doc
            Documents.UploadDocumentByParent();

            //Delete doc
            Documents.DeleteDocumentByParent();

            //Run method logout
            loginPage.Logout();


        }

        [Test]
        public void EditDocumentByParent_CorrectData_SucessResult()
        {
            // Login as a parent
            var loginPage = new Login(driver, Users.Parent1);

            //Open doc page
            var Documents = new Documents(driver, Users.Parent1);

            //Create doc
            Documents.UploadDocumentByParent();

            //Edit doc
            Documents.EditDocumentByParent();

            //Delete doc
            Documents.DeleteEditedDocumentByParent();

            //Run method logout
            loginPage.Logout();

        }

        [Test]
        public void DeleteDocumentByParent_CorrectData_SucessResult()
        {
            // Login as a parent
            var loginPage = new Login(driver, Users.Parent1);

            //Open doc page
            var Documents = new Documents(driver, Users.Parent1);

            //Create doc
            Documents.UploadDocumentByParent();

            //Delete doc
            Documents.DeleteDocumentByParent();

            //Run method logout
            loginPage.Logout();

        }
    }
}

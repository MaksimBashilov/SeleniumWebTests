using Maksim.Web.SeleniumTests.Pages;
using NUnit.Framework;

namespace Maksim.Web.SeleniumTests.Area.Teacher
{
    [TestFixture]
    class TeacherClassLog : BaseTest
    {
        private BaseTest baseTest;

        [SetUp]
        public void SetUp()
        {
            baseTest = new BaseTest();
        }

        [TearDown]
        public void Cleanup()
        {
            baseTest.CleanUp();
        }


        [Test, Description("Create ClassLog")]
        public void CreateClassLog()
        {
            // Login as a teacher
            var q = new Login(driver, Users.Teacher1);

            //Go to classlog page
            ClassLog ClassLogPage = new ClassLog(driver);

            // Add security code
            ClassLogPage.AddNativeCode("123456");

            ClassLogPage.CreateNote(Student1, "Maxim Soap", "Selenium class log note text", "Selenium Category");

            q.NewLogout();
        }

        [Test, Description("Login by Unilogin")]
        public void ClassLogUniLogin()
        {
            // Login as a teacher by unilogin
            var loginPage = new UniLogin(driver, Users.Teacher1, "kurs0325", "sxy56qhs");

            //Go to classlog page
            ClassLog ClassLogPage = new ClassLog(driver);

            // Add security UniLogin code
            ClassLogPage.AddUniLogineCode(driver, "kurs0325", "sxy56qhs");

            loginPage.NewLogout();
        }

        [Test, Description("Edit ClassLog")]
        public void EditClassLog()
        {
            // Login as a teacher
            var q = new Login(driver, Users.Teacher1);

            //Go to classlog page
            ClassLog ClassLogPage = new ClassLog(driver);

            // Add security code
            ClassLogPage.AddNativeCode("123456");

            ClassLogPage.CreateNote(Student1, "Maxim Soap", "Selenium class log note text", "Selenium Category");

            ClassLogPage.EditNote(".Edited");

            q.NewLogout();
        }


        [Test, Description("Delete ClassLog")]
        public void DeleteClassLog()
        {
            // Login as a teacher
            var q = new Login(driver, Users.Teacher1);

            //Go to classlog page
            ClassLog ClassLogPage = new ClassLog(driver);

            // Add security code
            ClassLogPage.AddNativeCode("123456");

            ClassLogPage.CreateNote(Student1, "Maxim Soap", "Selenium class log note text", "Selenium Category");

            ClassLogPage.DeleteNote();

            q.NewLogout();
        }

        [Test, Description("Add attachments to ClassLog")]
        public void AddAttachmentsClassLog()
        {
            // Login as a teacher
            var q = new Login(driver, Users.Teacher1);

            //Go to classlog page
            ClassLog ClassLogPage = new ClassLog(driver);

            // Add security code
            ClassLogPage.AddNativeCode("123456");

            ClassLogPage.AddAttachmentsNote(Student1, "Maxim Soap", "Selenium class log note text", "Selenium Category");

            q.NewLogout();
        }

    }
}

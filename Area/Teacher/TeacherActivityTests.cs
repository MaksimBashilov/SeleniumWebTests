using Maksim.Web.SeleniumTests.Pages;
using NUnit.Framework;



namespace Maksim.Web.SeleniumTests.Area.Teacher
{
    [TestFixture]
    class TeacherActivityTests : BaseTest
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

        public void CreateActivityByTeacher_CorrectData_SucessResult()
        {
            // Login as a teacher
           var loginPage =  new Login(driver, Users.Teacher1);

            //Open activvity page
            var Activity = new Activity(driver);

            //Create activity
            Activity.CreateActivity();

            //delete activity with no undo
            Activity.DeleteCreatedActivityNoUndo();

            //Run method logout
            loginPage.Logout();

        }


        [Test]

        public void CreateActivityByTeacher_ConfirmationMessage_SucessResult()
        {
            // Login as a teacher
            var loginPage = new Login(driver, Users.Teacher1);

            //Open activvity page
            var Activity = new Activity(driver);

            //Create activity with confirmation
            Activity.CreateActivityWithConfirmation();

            //delete activity with no undo
            Activity.DeleteCreatedActivityNoUndo();

            //Run method logout
            loginPage.Logout();

        }


        [Test]

        public void CreateActivityByTeacher_NoClass_FailResult()
        {
            // Login as a teacher
            var loginPage = new Login(driver, Users.Teacher1);

            //Open activvity page
            var Activity = new Activity(driver);

            //Create activity with no class
            Activity.CreateActivityNoClass();

            //Run method logout
            loginPage.Logout();

        }

        [Test]

        public void CreateActivityByTeacher_EditNoUndo_SucessResult()
        {
            // Login as a teacher
            var loginPage = new Login(driver, Users.Teacher1);

            //Open activvity page
            var Activity = new Activity(driver);

            //edit activity with no undo
            Activity.EditActivityNoUndo();

            //delete activity with no undo
            Activity.DeleteEditedActivityNoUndo();

            //Run method logout
            loginPage.Logout();

        }

    [Test]

    public void CreateActivityByTeacher_EditWithUndo_SucessResult()
    {
            // Login as a teacher
        var loginPage = new Login(driver, Users.Teacher1);

           //Open activvity page
        var Activity = new Activity(driver);

           //edit activity with undo
        Activity.EditActivityWithUndo();

        //delete activity with no undo
        Activity.DeleteEditedUndoActivityNoUndo();

            //Run method logout
            loginPage.Logout();

        }

        [Test]

        public void CreateActivityByTeacher_DeleteNoUndo_SucessResult()
        {
            // Login as a teacher
            var loginPage = new Login(driver, Users.Teacher1);

            //Open activvity page
            var Activity = new Activity(driver);

            //delete activity with no undo
            Activity.DeleteActivityNoUndo();

            //Run method logout
            loginPage.Logout();

        }

        [Test]

        public void CreateActivityByTeacher_DeleteWithUndo_SucessResult()
        {
            // Login as a teacher
            var loginPage = new Login(driver, Users.Teacher1);

            //Open activvity page
            var Activity = new Activity(driver);

            //delete activity with no undo
            Activity.DeleteActivityWithUndo();

            //delete activity with no undo
            Activity.DeleteDeletedUndoActivity();

            //Run method logout
            loginPage.Logout();

        }

    }
}

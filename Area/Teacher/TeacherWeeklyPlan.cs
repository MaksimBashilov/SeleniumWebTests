using Maksim.Web.SeleniumTests.Pages;
using NUnit.Framework;

namespace Maksim.Web.SeleniumTests.Area.Teacher
{
    [TestFixture]
    class TeacherWeeklyPlan : BaseTest
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


        [Test, Description("Create WeeklyPlan")]
        public void CreateWeeklyPlan()
        {
            // Login as a teacher
            var login = new Login(driver, Users.Teacher1);

            //Go to weekly plan page
            WeeklyPlan weeklyPlanPage = new WeeklyPlan(driver);

            // Create document based weekly plan
            weeklyPlanPage.CreateDocumentPlan("0Å", "Selenium Weekly Plan", "43-2018");

            //delete plan
            weeklyPlanPage.DeleteDocumentPlan();

            login.NewLogout();
        }

        [Test, Description("Edit WeeklyPlan")]
        public void EditWeeklyPlan()
        {
            // Login as a teacher
            var login = new Login(driver, Users.Teacher1);

            //Go to weekly plan page
            WeeklyPlan weeklyPlanPage = new WeeklyPlan(driver);

            // Create document based weekly plan
            weeklyPlanPage.CreateDocumentPlan("0Å", "Selenium Weekly Plan", "43-2018");

            //edit plan
            weeklyPlanPage.EditDocumentPlan(".Edited");

            //delete plan
            weeklyPlanPage.DeleteDocumentPlan();

            login.NewLogout();
        }

        [Test, Description("Delete WeeklyPlan")]
        public void DeleteWeeklyPlan()
        {
            // Login as a teacher
            var login = new Login(driver, Users.Teacher1);

            //Go to weekly plan page
            WeeklyPlan weeklyPlanPage = new WeeklyPlan(driver);

            // Create document based weekly plan
            weeklyPlanPage.CreateDocumentPlan("0Å", "Selenium Weekly Plan", "43-2018");

            //delete plan
            weeklyPlanPage.DeleteDocumentPlan();

            login.NewLogout();
        }

        [Test, Description("Create and Publish WeeklyPlan")]
        public void CreatePublishWeeklyPlan()
        {
            // Login as a teacher
            var login = new Login(driver, Users.Teacher1);

            //Go to weekly plan page
            WeeklyPlan weeklyPlanPage = new WeeklyPlan(driver);

            // Create document based weekly plan
            weeklyPlanPage.CreateDocumentPlan("0Å", "Selenium Weekly Plan", "43-2018");

            //Publish plan
            weeklyPlanPage.PublishDocumentPlan();

            //delete plan
            weeklyPlanPage.DeleteDocumentPlan();

            login.NewLogout();
        }


        [Test, Description("Redirect Template WeeklyPlan")]
        public void RedirectTemplateWeeklyPlan()
        {
            // Login as a teacher
            var login = new Login(driver, Users.Teacher1);

            //Go to weekly plan page
            WeeklyPlan weeklyPlanPage = new WeeklyPlan(driver);

            // Create document based weekly plan
            weeklyPlanPage.OpenTemplatePlan("1Z", "43-2018");

            login.NewLogout();
        }
    }
}

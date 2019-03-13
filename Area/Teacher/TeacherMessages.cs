using Maksim.Web.SeleniumTests.Pages;
using NUnit.Framework;

namespace Maksim.Web.SeleniumTests.Area.Teacher
{
    [TestFixture]
    class TeacherMessages : BaseTest
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

        [Test, Description("Testing Messages > All tabs are there")]
        public void MessagesAllTabs()
        {
            // Login as a teacher
            new Login(driver, Users.Teacher);
            Messages messagesPage = new Messages(driver, Users.Teacher);

            // Check all Messages tabs for existence
            messagesPage.SwitchTo(Tabs.Conversations);
            messagesPage.SwitchTo(Tabs.Outbox);
            messagesPage.SwitchTo(Tabs.Draft);
            messagesPage.SwitchTo(Tabs.Unread);
            messagesPage.SwitchTo(Tabs.Archive);
        }

        [Test, Description("Testing Messages > Teacher Create new message and check receipt")]
        public void MessagesCreateNewByTeacher()
        {
            // Login as a teacher
            new Login(driver, Users.Teacher);
            Messages messagesPage = new Messages(driver, Users.Teacher);

            // Create message for Teacher2
            messagesPage.NewMessage(Teacher2, "Selenium Subject", "It is a body Selenium");

            // Check that Teacher2 received this message
            messagesPage.OpenMessage(Teacher2, "Selenium Subject", "It is a body Selenium");
        }

        [Test, Description("Testing Messages > Teacher Delete new message")]
        public void MessagesDeleteByTeacher()
        {
            // Login as a teacher
            new Login(driver, Users.Teacher);
            Messages messagesPage = new Messages(driver, Users.Teacher);

            // Create message for Teacher2
            messagesPage.NewMessage(Teacher1, "Selenium Delete Subject", "It is a body Selenium");

            messagesPage.TopMenuDelete(Teacher1, "Selenium Delete Subject");

        }

    }
}

using Maksim.Web.SeleniumTests.Pages;
using NUnit.Framework;

namespace Maksim.Web.SeleniumTests.Area.Parent
{
    [TestFixture]
    class ParentMessages : BaseTest
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
            new Login(driver, Users.Parent);
            Messages messagesPage = new Messages(driver, Users.Parent);

            // Check all Messages tabs for existence
            messagesPage.SwitchTo(Tabs.Conversations);
            messagesPage.SwitchTo(Tabs.Outbox);
            messagesPage.SwitchTo(Tabs.Draft);
            messagesPage.SwitchTo(Tabs.Unread);
            messagesPage.SwitchTo(Tabs.Archive);
        }


        [Test, Description("Testing Messages > Parent Create new message and check receipt")]
        public void MessagesCreateNewByParent_CorrectData_SucessResult()
        {
            // Login as a teacher
            new Login(driver, Users.Parent);
            Messages messagesPage = new Messages(driver, Users.Parent);

            // Create message for Teacher2
            messagesPage.NewMessage(Teacher2, "Selenium Subject", "It is a body Selenium");

            // Check that Teacher2 received this message
            messagesPage.OpenMessage(Teacher2, "Selenium Subject", "It is a body Selenium");
        }

    }
}

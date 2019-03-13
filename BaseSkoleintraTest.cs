using System;
using System.Configuration;
using Maksim.SeleniumTests;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Maksim.Web.SeleniumTests
{
    public class BaseTest
    {
        protected string TestHost => ConfigurationManager.AppSettings["TestHost"];
        protected string TestHost => ConfigurationManager.AppSettings["TestHost"];
        protected string TestUrl => ConfigurationManager.AppSettings["TestUrl"];
        protected string TeacherLogin => ConfigurationManager.AppSettings["TeacherLogin"];
        protected string TeacherPassword => ConfigurationManager.AppSettings["TeacherPassword"];
        protected string ParentLogin => ConfigurationManager.AppSettings["ParentLogin"];
        protected string ParentPassword => ConfigurationManager.AppSettings["ParentPassword"];
        protected string Parent2Login => ConfigurationManager.AppSettings["Parent2Login"];
        protected string Parent2Password => ConfigurationManager.AppSettings["Parent2Password"];
        protected string TeacherLoginMb => ConfigurationManager.AppSettings["TeacherLoginMb"];
        protected string TeacherPasswordMb => ConfigurationManager.AppSettings["TeacherPasswordMb"];
        protected string TeacherMbDisplayName => ConfigurationManager.AppSettings["TeacherMbDisplayName"];
        protected string ParentLoginMax => ConfigurationManager.AppSettings["ParentLoginMax"];
        protected string ParentPasswordMax => ConfigurationManager.AppSettings["ParentPasswordMax"];
        protected string StudentLogin => ConfigurationManager.AppSettings["StudentLogin"];
        protected string StudentDisplayName => ConfigurationManager.AppSettings["StudentDisplayName"];
        protected string StudentPassword => ConfigurationManager.AppSettings["StudentPassword"];
        protected UserEntity Teacher1, Teacher2, Parent1, Parent2, Student1, Student2;

        public static IWebDriver driver;

        public BaseTest()
        {
            if (driver == null)
            {
                driver = new ChromeDriver();
            }

            Teacher1 = new UserEntity(ConfigurationManager.AppSettings["TeacherLogin"], ConfigurationManager.AppSettings["TeacherPassword"], ConfigurationManager.AppSettings["TeacherDisplayName"]);
            Teacher2 = new UserEntity(ConfigurationManager.AppSettings["TeacherLoginMb"], ConfigurationManager.AppSettings["TeacherPasswordMb"], ConfigurationManager.AppSettings["TeacherMbDisplayName"]);
            Student1 = new UserEntity(ConfigurationManager.AppSettings["StudentLogin"], ConfigurationManager.AppSettings["StudentPassword"], ConfigurationManager.AppSettings["StudentDisplayName"]);
        }

        public void CleanUp()
        {
            driver.Close();
            driver.Dispose();
            driver.Quit();
            driver = null;
        }

        public static Func<IWebDriver, bool> ElementIsVisible(IWebElement element)
        {
            return (_driver) =>
            {
                try
                {
                    return element.Displayed;
                }
                catch (Exception)
                {
                    // If element is null, stale or if it cannot be located
                    return false;
                }
            };
        }
    }
}
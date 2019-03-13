using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Maksim.Web.SeleniumTests.Pages
{
    enum Users
    {
        Teacher,
        Teacher1,
        Parent,
        Parent1,
        Parent2,
        Student
    }

    class Login : BaseTest
    {
        private IWebDriver _driver;

        public Login(IWebDriver driver, Users user)
        {
            _driver = driver;

            switch (user)
            {
                case Users.Teacher:
                    _driver.Navigate().GoToUrl($"{TestHost}Account/IdpLogin?role=Teacher");
                    _driver.FindElement(By.Id("UserName")).SendKeys(TeacherLogin);
                    _driver.FindElement(By.Id("Password")).SendKeys(TeacherPassword);
                    break;
                case Users.Teacher1:
                    _driver.Navigate().GoToUrl($"{TestHost}Account/IdpLogin?role=Teacher");
                    _driver.FindElement(By.Id("UserName")).SendKeys(TeacherLoginMb);
                    _driver.FindElement(By.Id("Password")).SendKeys(TeacherPasswordMb);
                    break;
                case Users.Parent:
                    _driver.Navigate().GoToUrl($"{TestHost}Account/IdpLogin?role=Parent");
                    _driver.FindElement(By.Id("UserName")).SendKeys(ParentLogin);
                    _driver.FindElement(By.Id("Password")).SendKeys(ParentPassword);
                    break;
                case Users.Parent1:
                    _driver.Navigate().GoToUrl($"{TestHost}Account/IdpLogin?role=Parent");
                    _driver.FindElement(By.Id("UserName")).SendKeys(ParentLoginMax);
                    _driver.FindElement(By.Id("Password")).SendKeys(ParentPasswordMax);
                    break;
                case Users.Parent2:
                    _driver.Navigate().GoToUrl($"{TestHost}Account/IdpLogin?role=Parent");
                    _driver.FindElement(By.Id("UserName")).SendKeys(Parent2Login);
                    _driver.FindElement(By.Id("Password")).SendKeys(Parent2Password);
                    break;
                case Users.Student:
                    _driver.Navigate().GoToUrl($"{TestHost}Account/IdpLogin?role=Student");
                    _driver.FindElement(By.Id("UserName")).SendKeys(StudentLogin);
                    _driver.FindElement(By.Id("Password")).SendKeys(StudentPassword);
                    break;
            }

            _driver.FindElement(By.Id("Password")).SendKeys(Keys.Enter);

            // Wait for button "Login" not exist
            var el = new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.InvisibilityOfElementWithText(By.ClassName("sk-login-button"), "Login"));

            Assert.IsTrue(el);
        }

        public void Logout()
        {
            _driver = driver;

            //Logging off
            _driver.FindElement(
                By.CssSelector("#sk-personal-menu-button > div > div.sk-personal-menu-button-text.h-fl-l")).Click();
            _driver.FindElement(By.ClassName("sk-personal-menu-item-logout")).Click();

            // Wait for button "Login" exist
            var el1 = new WebDriverWait(_driver, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.ClassName("sk-login-button")));

            Assert.IsTrue(el1.GetAttribute("class").Contains("sk-login-button"));
        }

        public void WrongPasswordParent()
        {
            //Check Error Feedback appear
            var feedback = new WebDriverWait(_driver, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.Id("ErrorSummary_text")));

            Assert.IsTrue(feedback.GetAttribute("class").Contains("ccl-feedback-text"));
        }


        public void NewLogout()
        {

            _driver = driver;

            var classname =
                _driver.FindElements(
                    By.ClassName("l-personal-menu-icon-useronline"));


            if (classname.Count == 1)
            {
                _driver.FindElement(By.CssSelector("#personal-menu-link")).Click();
                _driver.FindElement(By.CssSelector("#personal-menu-dd > li.l-dropdown-menu-item.l-pm-logout > a"))
                    .Click();
            }

            else
            {
                _driver.FindElement(
                    By.CssSelector("#sk-personal-menu-button > div > div.sk-personal-menu-button-text.h-fl-l")).Click();
                _driver.FindElement(By.ClassName("sk-personal-menu-item-logout")).Click();
            }

            // Wait for button "Login" exist
            var el1 = new WebDriverWait(_driver, TimeSpan.FromSeconds(30)).Until(
                ExpectedConditions.ElementExists(By.ClassName("sk-login-button")));

            Assert.IsTrue(el1.GetAttribute("class").Contains("sk-login-button"));

        }


    }

    class UniLogin : BaseTest
    {
        private IWebDriver _driver;
        public UniLogin(IWebDriver driver, Users user, string login, string password)
        {
            _driver = driver;

            switch (user)
            {
                case Users.Teacher:
                    _driver.Navigate().GoToUrl($"{TestHost}Account/IdpLogin?role=Teacher");
                    break;
                case Users.Teacher1:
                    _driver.Navigate().GoToUrl($"{TestHost}Account/IdpLogin?role=Teacher");
                    break;
                case Users.Parent:
                    _driver.Navigate().GoToUrl($"{TestHost}Account/IdpLogin?role=Parent");
                    break;
                case Users.Parent1:
                    _driver.Navigate().GoToUrl($"{TestHost}Account/IdpLogin?role=Parent");
                    break;
                case Users.Parent2:
                    _driver.Navigate().GoToUrl($"{TestHost}Account/IdpLogin?role=Parent");
                    break;
                case Users.Student:
                    _driver.Navigate().GoToUrl($"{TestHost}Account/IdpLogin?role=Student");
                    break;
            }


            // Press on a UniLogin button
            _driver.FindElement(By.ClassName("sk-uni-login-button")).Click();


            // Wait for button "Login" not exist
            var el = new WebDriverWait(_driver, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.InvisibilityOfElementWithText(By.ClassName("sk-login-button"), "Login"));

            Assert.IsTrue(el);

            // Add login and password
            _driver.FindElement(By.Id("user")).SendKeys(login);
            _driver.FindElement(By.Id("pass")).SendKeys(password);
            _driver.FindElement(By.Id("pass")).SendKeys(Keys.Enter);

            //Check that you don't see password field
            var el1 = new WebDriverWait(_driver, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.InvisibilityOfElementWithText(By.Id("login"), "login"));

            Assert.IsTrue(el1);

        }

        public void NewLogout()
        {

            _driver = driver;

            var classname =
                _driver.FindElements(
                    By.ClassName("l-personal-menu-icon-useronline"));


            if (classname.Count == 1)
            {
                _driver.FindElement(By.CssSelector("#personal-menu-link")).Click();
                _driver.FindElement(By.CssSelector("#personal-menu-dd > li.l-dropdown-menu-item.l-pm-logout > a"))
                    .Click();
            }

            else
            {
                _driver.FindElement(
                    By.CssSelector("#sk-personal-menu-button > div > div.sk-personal-menu-button-text.h-fl-l")).Click();
                _driver.FindElement(By.ClassName("sk-personal-menu-item-logout")).Click();
            }

            // Wait for button "Login" exist
            var el1 = new WebDriverWait(_driver, TimeSpan.FromSeconds(30)).Until(
                ExpectedConditions.ElementExists(By.ClassName("sk-login-button")));

            Assert.IsTrue(el1.GetAttribute("class").Contains("sk-login-button"));

        }
    }

}



using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;

namespace Maksim.Web.SeleniumTests.Pages
{
    internal class ContactBook : BaseTest
    {
        private IWebDriver _driverGC;
        private int _randomText;
        private string _contactBookNameParent;
        private string _contactBookNameTeacher;

        //Press Create new note
        [FindsBy(How = How.ClassName,Using ="sk-button-style-create")]
        private IWebElement CreateContactBookButton { get; set; }

        public ContactBook(IWebDriver driver, Users userrole)
        {
            _driverGC = driver;
            PageFactory.InitElements(_driverGC, this);

            //add random digit to a contact book name
            Random rnd = new Random();
            _randomText = rnd.Next(1, 100);

            //save pin name and random digit
            _contactBookNameParent = $"Selenium Contact book test Parent {_randomText}";

            //save pin name and random digit
            _contactBookNameTeacher = $"Selenium Contact book test Teacher {_randomText}";

            switch (userrole)
            {
                case Users.Parent1:
                    // Go to  Contact book Page
                    _driverGC.FindElement(By.XPath("/html/body/div[1]/div[1]/div/div[1]/nav/ul/li[3]")).Click(); 
                    break;

                case Users.Teacher1:
                    // Go to  Contact book Page
                    _driverGC.Navigate().GoToUrl($"{TestHost}/staff/contactbook/all");
                    _driverGC.FindElement(By.LinkText($"{StudentDisplayName}")).Click();
                    break;
            }


            //Wait for page loading
            _driverGC.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);

        }


        public void ContactBookNoteParent()
        {

            // Press Create button
            _driverGC.FindElement(By.ClassName("sk-button-style-create")).Click();

            // wait for a create page loading
            var element = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.XPath(".//*[@id='cke_1_top']")));

            Assert.IsTrue(element.GetAttribute("Class").Contains("cke_top"));

            // Add text to iframe
            IWebElement frame = _driverGC.FindElement(By.ClassName("cke_wysiwyg_frame"));
            _driverGC.SwitchTo().Frame(frame);
            _driverGC.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            _driverGC.FindElement(By.XPath("/html/body")).Click();

            //Add this name to a pin
            _driverGC.FindElement(By.XPath("html/body")).SendKeys(_contactBookNameParent);

            // Back from iframe 
            _driverGC.SwitchTo().Window(_driverGC.CurrentWindowHandle);

            // Choose teacher check box
            _driverGC.FindElement(By.XPath("//*[contains(text(), '" + TeacherMbDisplayName + "')]")).Click();

            //Add category name
                _driverGC.FindElement(By.CssSelector("#ContactBookNote_CategoryName")).Click();
                _driverGC.FindElement(By.CssSelector(
                        "#ExtensibleOptionsList > div > div.sk-eol-dropdown-list > div.sk-eol-create-option-section > button"))
                    .Click();
                _driverGC.FindElement(By.CssSelector(
                        "#ExtensibleOptionsList > div > div.sk-eol-dropdown-list > div.sk-eol-create-option-section > div > input[type=\"text\"]"))
                    .SendKeys("Selenium Category");
                _driverGC.FindElement(By.CssSelector(
                        "#ExtensibleOptionsList > div > div.sk-eol-dropdown-list > div.sk-eol-create-option-section > div > input[type=\"text\"]"))
                    .SendKeys(Keys.Enter);

            //Press Save
            _driverGC.FindElement(By.XPath("//*[starts-with(@id, 'OkOrCancel') and @class=\"ccl-button sk-button-blue sk-font-icon sk-button-text-only\"]")).Click();

            //wait for a contact book page for a student is loaded
            var contactbookpageloaded = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.Id("sk-contactbook-notes-content-container")));

            Assert.IsTrue(contactbookpageloaded.GetAttribute("class").Contains("sk-grid"));

            //Check that contact book is created
            var test =
                $"//*[@class=\"sk-contactbook-note sk-user-input\" and contains(text(),'{_contactBookNameParent}')]";

            var contactbookcreated = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.XPath(test)));

            Assert.AreEqual(true, contactbookcreated.FindElement(By.XPath(test)).Displayed);

        }

        public void ContactBookNoteCommentParent()
        {
            //Press Reply on this note 
            var testreply =
                $"//div[@id=\"sk-contactbook-notes-content-container\"]//div[contains(.,'{_contactBookNameParent}')]/../../li[2]/div/a";
            _driverGC.FindElement(By.XPath(testreply)).Click();

            // wait for a create page loading
            var element = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.XPath(".//*[@id='cke_1_top']")));

            Assert.IsTrue(element.GetAttribute("Class").Contains("cke_top"));

            //Check Contactboo note name is correct

            var test = $"//*[contains(text(), '{_contactBookNameParent}')]";
            var contactbookonapage = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.XPath(test)));

            Assert.AreEqual(true, contactbookonapage.FindElement(By.XPath(test)).Displayed);

            // Add text to iframe
            IWebElement frame = _driverGC.FindElement(By.ClassName("cke_wysiwyg_frame"));
            _driverGC.SwitchTo().Frame(frame);
            _driverGC.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            _driverGC.FindElement(By.XPath("/html/body")).Click();

            //save note name and random digit
            var contactBookReplyName = $"Selenium Contact Book Note Reply Parent {_randomText}";

            //Add reply name to a note
            _driverGC.FindElement(By.XPath("html/body")).SendKeys(contactBookReplyName);

            // Back from iframe 
            _driverGC.SwitchTo().Window(_driverGC.CurrentWindowHandle);

            // Choose teacher check box
            _driverGC.FindElement(By.XPath("//*[contains(text(), '" + TeacherMbDisplayName + "')]")).Click();

            //Press Save
            _driverGC.FindElement(By.XPath("//*[starts-with(@id, 'OkOrCancel') and @class=\"ccl-button sk-button-blue sk-font-icon sk-button-text-only\"]")).Click();

            //wait for a contact book page for a student is loaded
            var contactbookpageloaded = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.Id("sk-contactbook-notes-content-container")));

            Assert.IsTrue(contactbookpageloaded.GetAttribute("class").Contains("sk-grid"));

            //Check that contact book is created
            var test1 =
                $"//div[@id=\"sk-contactbook-notes-content-container\"]//div[contains(.,'{_contactBookNameParent}')]/../../../ul[2]/li/div[contains(.,'{contactBookReplyName}')]";

            var contactbookreplycreated = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.XPath(test1)));

            Assert.AreEqual(true, contactbookreplycreated.FindElement(By.XPath(test1)).Displayed);

        }


        public void ContactBookNoteTeacher()
        {
            //Press Create new note
            _driverGC.FindElement(By.ClassName("sk-button-style-create")).Click();

            // wait for a create page loading
            var element = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.XPath(".//*[@id='cke_1_top']")));

            Assert.IsTrue(element.GetAttribute("Class").Contains("cke_top"));

            // Add text to iframe
            IWebElement frame = _driverGC.FindElement(By.ClassName("cke_wysiwyg_frame"));
            _driverGC.SwitchTo().Frame(frame);
            _driverGC.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            _driverGC.FindElement(By.XPath("/html/body")).Click();

            //Add this name to a pin
            _driverGC.FindElement(By.XPath("html/body")).SendKeys(_contactBookNameTeacher);

            // Back from iframe 
            _driverGC.SwitchTo().Window(_driverGC.CurrentWindowHandle);

            //Add category name
            _driverGC.FindElement(By.CssSelector("#ContactBookNote_CategoryName")).Click();
            _driverGC.FindElement(By.CssSelector(
                    "#ExtensibleOptionsList > div > div.sk-eol-dropdown-list > div.sk-eol-create-option-section > button"))
                .Click();
            _driverGC.FindElement(By.CssSelector(
                    "#ExtensibleOptionsList > div > div.sk-eol-dropdown-list > div.sk-eol-create-option-section > div > input[type=\"text\"]"))
                .SendKeys("Selenium Category");
            _driverGC.FindElement(By.CssSelector(
                    "#ExtensibleOptionsList > div > div.sk-eol-dropdown-list > div.sk-eol-create-option-section > div > input[type=\"text\"]"))
                .SendKeys(Keys.Enter);

            //Press Save
            _driverGC.FindElement(By.XPath("//*[starts-with(@id, 'OkOrCancel') and @class=\"ccl-button sk-button-blue sk-font-icon sk-button-text-only\"]")).Click();

            //wait for a contact book page for a student is loaded
            var contactbookpageloaded = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.Id("sk-contactbook-notes-content-container")));

            Assert.IsTrue(contactbookpageloaded.GetAttribute("class").Contains("sk-grid"));

            //Check that contact book is created
            var test =
                $"//*[@class=\"sk-contactbook-note sk-user-input\" and contains(text(),'{_contactBookNameTeacher}')]";

            var contactbookcreated = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.XPath(test)));

            Assert.AreEqual(true, contactbookcreated.FindElement(By.XPath(test)).Displayed);

        }

        public void ContactBookNoteCommentTeacher()
        {

            //Press Reply on this note 
            var testreply =
                $"//div[@id=\"sk-contactbook-notes-content-container\"]//div[contains(.,'{_contactBookNameTeacher}')]/../../li[2]/div/a";
            _driverGC.FindElement(By.XPath(testreply)).Click();

            // wait for a create page loading
            var element = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.XPath(".//*[@id='cke_1_top']")));

            Assert.IsTrue(element.GetAttribute("Class").Contains("cke_top"));

            // Add text to iframe
            IWebElement frame = _driverGC.FindElement(By.ClassName("cke_wysiwyg_frame"));
            _driverGC.SwitchTo().Frame(frame);
            _driverGC.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            _driverGC.FindElement(By.XPath("/html/body")).Click();

            //save note name and random digit
            var contactBookReplyName = $"Selenium Contact Book Note Reply Teacher {_randomText}";

            //Add reply name to a note
            _driverGC.FindElement(By.XPath("html/body")).SendKeys(contactBookReplyName);

            // Back from iframe 
            _driverGC.SwitchTo().Window(_driverGC.CurrentWindowHandle);

            //Press Save
            _driverGC.FindElement(By.XPath("//*[starts-with(@id, 'OkOrCancel') and @class=\"ccl-button sk-button-blue sk-font-icon sk-button-text-only\"]")).Click();

            //wait for a contact book page for a student is loaded
            var contactbookpageloaded = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.Id("sk-contactbook-notes-content-container")));

            Assert.IsTrue(contactbookpageloaded.GetAttribute("class").Contains("sk-grid"));

            //Check that contact book is created
            var test1 =
                $"//div[@id=\"sk-contactbook-notes-content-container\"]//div[contains(.,'{_contactBookNameTeacher}')]/../../../ul[2]/li/div[contains(.,'{contactBookReplyName}')]";

            var contactbookreplycreated = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.XPath(test1)));

            Assert.AreEqual(true, contactbookreplycreated.FindElement(By.XPath(test1)).Displayed);

        }

        public void ContactBookNoteTeacherCancel()
        {
            //Press Create new note
            _driverGC.FindElement(By.ClassName("sk-button-style-create")).Click();

            // wait for a create page loading
            var element = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.XPath(".//*[@id='cke_1_top']")));

            Assert.IsTrue(element.GetAttribute("Class").Contains("cke_top"));

            //Press Cancel
            _driverGC.FindElement(By.XPath("//*[starts-with(@id, 'OkOrCancel') and @class=\"ccl-button sk-button-white sk-font-icon sk-button-text-only\"]")).Click();

            //wait for a contact book page for a student is loaded
            var buttonsave = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.InvisibilityOfElementLocated(By.ClassName("sk-button-style-create")));

            Assert.IsTrue(buttonsave);
        }
    }
}
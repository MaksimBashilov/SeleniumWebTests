using System;
using System.Collections.ObjectModel;
using Web.UI.Controls.Common.Utils;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;


namespace Maksim.Web.SeleniumTests.Pages
{
    class Pin : BaseTest
    {
        private IWebDriver _driverGC;

        public Pin(IWebDriver driver, Users userrole)
        {
            _driverGC = driver;
            PageFactory.InitElements(_driverGC, this);

            switch (userrole)
            {
                case Users.Parent1:
                    _driverGC.FindElement(By.CssSelector("body > div.sk-l-main-contaier > div.sk-l-content-wrapper > div > div.sk-homepage > div.sk-homepage-newscontainer > div > div.sk-homepage-news-links-block > button")).Click();
                    break;
                case Users.Teacher1:
                    _driverGC.Navigate().GoToUrl($"{TestHost}/staff/news/pins/create?returnUrl=%2Fstaff%2FIndex");
                    break;
            }

            var element = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(ExpectedConditions.ElementExists(By.XPath(".//*[@id='cke_1_top']")));
            Assert.IsTrue(element.GetAttribute("Class").Contains("cke_top"));

        }

        public void CreatePinByParent()
        {

            // Add text to iframe
            IWebElement frame = _driverGC.FindElement(By.ClassName("cke_wysiwyg_frame"));
            _driverGC.SwitchTo().Frame(frame);
            _driverGC.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            _driverGC.FindElement(By.XPath("/html/body")).Click();
            _driverGC.FindElement(By.XPath("html/body")).SendKeys("Selenium pin test");

            // Back from iframe and save
            _driverGC.SwitchTo().Window(_driverGC.CurrentWindowHandle);
            _driverGC.FindElement(By.XPath("//div[2]/div/div/input")).Click();

            //Check Feedback appear
            var feedback = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.XPath(".//*[@id='genericFeedbackBaseLayout_text']")));

            Assert.IsTrue(feedback.GetAttribute("class").Contains(CommonCssClasses.Controls.Feedback.Text));
        }

        public void EditPinByParent()
        {
            //Open created pin for edit
            ReadOnlyCollection<IWebElement> list = _driverGC.FindElements(By.XPath("//a[contains(@data-info,'Selenium pin test')]"));
            list[0].Click();
            _driverGC.FindElement(By.XPath(".//*[@id='sk-pin-edit-link']/div/div")).Click();

            //Wait for editor top menu appear on a pin page
            var element = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.XPath(".//*[@id='cke_1_top']")));

            Assert.IsTrue(element.GetAttribute("Class").Contains("cke_top"));

            // Add text to iframe
            IWebElement frame = _driverGC.FindElement(By.ClassName("cke_wysiwyg_frame"));
            _driverGC.SwitchTo().Frame(frame);
            _driverGC.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            _driverGC.FindElement(By.XPath("/html/body")).Click();
            _driverGC.FindElement(By.XPath("html/body")).SendKeys(".Edited");

            // Back from iframe and save
            _driverGC.SwitchTo().Window(_driverGC.CurrentWindowHandle);
            _driverGC.FindElement(By.XPath("//div[2]/div/div/input")).Click();

            //Check Feedback appear
            var feedback = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.XPath(".//*[@id='genericFeedbackBaseLayout_text']")));

            Assert.IsTrue(feedback.GetAttribute("class").Contains(CommonCssClasses.Controls.Feedback.Text));

            // Check that pin appear with correct text
            var pinexist = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.XPath("//a[contains(@data-info,'Selenium pin test.Edited')]")));

            Assert.AreEqual(true, pinexist.FindElement(By.XPath("//a[contains(@data-info,'Selenium pin test.Edited')]")).Displayed);
        }


        public void DeletePinByParent()
        {

            // Add text to iframe
            IWebElement frame = _driverGC.FindElement(By.ClassName("cke_wysiwyg_frame"));
            _driverGC.SwitchTo().Frame(frame);
            _driverGC.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            _driverGC.FindElement(By.XPath("/html/body")).Click();

            //add random digit to a pin name
            Random rnd = new Random();
            int deletetext = rnd.Next(1, 13);

            // save name with random digit 
            var pinName = $"Selenium pin test {deletetext}";

            //Add pin name on a page
            _driverGC.FindElement(By.XPath("html/body")).SendKeys(pinName);

            // Back from iframe and save
            _driverGC.SwitchTo().Window(_driverGC.CurrentWindowHandle);
            _driverGC.FindElement(By.XPath("//div[2]/div/div/input")).Click();

            //Check Feedback appear
            var feedback = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.XPath(".//*[@id='genericFeedbackBaseLayout_text']")));

            Assert.IsTrue(feedback.GetAttribute("class").Contains(CommonCssClasses.Controls.Feedback.Text));

            //Open pop up menu for delete
            ReadOnlyCollection<IWebElement> list = _driverGC.FindElements(By.XPath($"//a[contains(@data-info,'{pinName}')]"));
            list[0].Click();
            _driverGC.FindElement(By.XPath(".//*[@id='sk-pin-delete-link']/div/div")).Click();

            // Wait for pop-up "Delete" appear
            var deletebutton = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.ClassName("sk-button-white")));

            Assert.IsTrue(deletebutton.GetAttribute("class").Contains("sk-button-white"));

            //Press Delete
            _driverGC.FindElement(By.Id("sk-news-delete-confirmationButtonOkButtonId")).Click();

            // Check that pin not exist
            var pinnotexist = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.InvisibilityOfElementWithText(By.XPath("//a[contains(@data-info)"), pinName));

            Assert.IsTrue(pinnotexist);
        }

        public void DeleteCreatedPinByParent()
        {

            //Open pop up menu for delete
            ReadOnlyCollection<IWebElement> list = _driverGC.FindElements(By.XPath($"//a[contains(@data-info,'Selenium pin test')]"));
            list[0].Click();
            _driverGC.FindElement(By.XPath(".//*[@id='sk-pin-delete-link']/div/div")).Click();

            // Wait for pop-up "Delete" appear
            var deletebutton = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.ClassName("sk-button-white")));

            Assert.IsTrue(deletebutton.GetAttribute("class").Contains("sk-button-white"));

            //Press Delete
            _driverGC.FindElement(By.Id("sk-news-delete-confirmationButtonOkButtonId")).Click();

            // Check that pin not exist

            var pinnotexist = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//a[contains(@data-info,'Selenium pin test')]")));

            Assert.IsTrue(pinnotexist);
        }

        public void DeleteEditedPinByParent()
        {

            //Open pop up menu for delete
            ReadOnlyCollection<IWebElement> list = _driverGC.FindElements(By.XPath($"//a[contains(@data-info,'Selenium pin test.Edited')]"));
            list[0].Click();
            _driverGC.FindElement(By.XPath(".//*[@id='sk-pin-delete-link']/div/div")).Click();

            // Wait for pop-up "Delete" appear
            var deletebutton = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.ClassName("sk-button-white")));

            Assert.IsTrue(deletebutton.GetAttribute("class").Contains("sk-button-white"));

            //Press Delete
            _driverGC.FindElement(By.Id("sk-news-delete-confirmationButtonOkButtonId")).Click();

            var pinnotexist = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//a[contains(@data-info,'Selenium pin test.Edited')]")));

            Assert.IsTrue(pinnotexist);
        }


        public void CreatePinByTeacher()
        {
            // Add text to iframe
            IWebElement frame = _driverGC.FindElement(By.ClassName("cke_wysiwyg_frame"));
            _driverGC.SwitchTo().Frame(frame);
            _driverGC.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            _driverGC.FindElement(By.XPath("/html/body")).Click();
            _driverGC.FindElement(By.XPath("html/body")).SendKeys("Selenium pin test");

            // Back from iframe 
            _driverGC.SwitchTo().Window(_driverGC.CurrentWindowHandle);

            // Choose All Classes
            _driverGC.FindElement(By.XPath(".//*[@id='Target']/option[1]")).Click();

            // Choose Checkboxes
            _driverGC.FindElement(By.XPath(".//*[@id='sk-publishingSettings-container']/ol/li[2]/div/div/ins")).Click();
            _driverGC.FindElement(By.XPath(".//*[@id='sk-publishingSettings-container']/ol/li[3]/div/div/ins")).Click();
            _driverGC.FindElement(By.XPath(".//*[@id='sk-publishingSettings-container']/ol/li[4]/div/div/ins")).Click();

            // Save
            _driverGC.FindElement(By.XPath("//div[2]/div/div/input")).Click();

            //Check Feedback appear
            var feedback = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(15)).Until(
                ExpectedConditions.ElementExists(By.XPath(".//*[@id='genericFeedbackBaseLayout_text']")));

            Assert.IsTrue(feedback.GetAttribute("class").Contains(CommonCssClasses.Controls.Feedback.Text));
        }

        public void EditPinByTeacher()
        {
            //Wait for loading pins at right panel
            var el2 = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.ClassName("sk-homepage-news-links-block")));

            Assert.IsTrue(el2.GetAttribute("Class").Contains("sk-homepage-news-links-block"));

            //Open created pin for edit
            ReadOnlyCollection<IWebElement> list = _driverGC.FindElements(By.XPath("//a[contains(@data-info,'Selenium pin test')]"));
            list[0].Click();
            _driverGC.FindElement(By.XPath(".//*[@id='sk-pin-edit-link']/div/div")).Click();

            //Wait for editor top menu appear on a pin page
            var element = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
            ExpectedConditions.ElementExists(By.XPath(".//*[@id='cke_1_top']")));

            Assert.IsTrue(element.GetAttribute("Class").Contains("cke_top"));

            // Add text to iframe
            IWebElement frame = _driverGC.FindElement(By.ClassName("cke_wysiwyg_frame"));
            _driverGC.SwitchTo().Frame(frame);
            _driverGC.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            _driverGC.FindElement(By.XPath("/html/body")).Click();
            _driverGC.FindElement(By.XPath("html/body")).SendKeys(".Edited");

            // Back from iframe and save
            _driverGC.SwitchTo().Window(_driverGC.CurrentWindowHandle);
            _driverGC.FindElement(By.XPath("//div[2]/div/div/input")).Click();

            //Check Feedback appear
            var feedback = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.XPath(".//*[@id='genericFeedbackBaseLayout_text']")));

            Assert.IsTrue(feedback.GetAttribute("class").Contains(CommonCssClasses.Controls.Feedback.Text));

            // Check that pin appear with correct text
            var pinexist = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(15)).Until(
                ExpectedConditions.ElementExists(By.XPath("//a[contains(@data-info,'Selenium pin test.Edited')]")));

            Assert.AreEqual(true, pinexist.FindElement(By.XPath("//a[contains(@data-info,'Selenium pin test.Edited')]")).Displayed);
        }

        public void DeletePinByTeacher()
        {
            // Add text to iframe
            IWebElement frame = _driverGC.FindElement(By.ClassName("cke_wysiwyg_frame"));
            _driverGC.SwitchTo().Frame(frame);
            _driverGC.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            _driverGC.FindElement(By.XPath("/html/body")).Click();


            //add random digitto a pin name
            Random rnd = new Random();
            int deletetext = rnd.Next(1, 13);

            //save pin name and random digit
            var pinName = $"Selenium pin test {deletetext}";

            //Add this name to a pin
            _driverGC.FindElement(By.XPath("html/body")).SendKeys(pinName);

            // Back from iframe 
            _driverGC.SwitchTo().Window(_driverGC.CurrentWindowHandle);

            // Choose All Classes
            _driverGC.FindElement(By.XPath(".//*[@id='Target']/option[1]")).Click();

            // Choose Checkboxes
            _driverGC.FindElement(By.XPath(".//*[@id='sk-publishingSettings-container']/ol/li[2]/div/div/ins")).Click();
            _driverGC.FindElement(By.XPath(".//*[@id='sk-publishingSettings-container']/ol/li[3]/div/div/ins")).Click();
            _driverGC.FindElement(By.XPath(".//*[@id='sk-publishingSettings-container']/ol/li[4]/div/div/ins")).Click();

            // Save
            _driverGC.FindElement(By.XPath("//div[2]/div/div/input")).Click();

            //Check Feedback appear
            var feedback = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(15)).Until(
                ExpectedConditions.ElementExists(By.XPath(".//*[@id='genericFeedbackBaseLayout_text']")));

            Assert.IsTrue(feedback.GetAttribute("class").Contains(CommonCssClasses.Controls.Feedback.Text));

            //Wait for loading pins at right panel
            var el2 = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(20)).Until(
                ExpectedConditions.ElementExists(By.ClassName("sk-homepage-news-links-block")));

            Assert.IsTrue(el2.GetAttribute("Class").Contains("sk-homepage-news-links-block"));

            //Open pop up menu for delete
            ReadOnlyCollection<IWebElement> list = _driverGC.FindElements(By.XPath("//a[contains(@data-info,'" + pinName + "')]"));
            list[0].Click();
            _driverGC.FindElement(By.XPath(".//*[@id='sk-pin-delete-link']/div/div")).Click();

            // Wait for pop-up "Delete" appear
            var deletebutton = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(15)).Until(
                ExpectedConditions.ElementExists(By.ClassName("sk-button-white")));

            Assert.IsTrue(deletebutton.GetAttribute("class").Contains("sk-button-white"));

            //Press Delete
            _driverGC.FindElement(By.Id("sk-news-delete-confirmationButtonOkButtonId")).Click();


            //Wait for loading pins at right panel
            var el3 = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(15)).Until(
                ExpectedConditions.ElementExists(By.ClassName("sk-homepage-news-links-block")));

            Assert.IsTrue(el3.GetAttribute("Class").Contains("sk-homepage-news-links-block"));

            // Check that pin not exist
            var pinnotexist = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(15)).Until(
                ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//a[contains(@data-info,'" + pinName + "')]")));

            Assert.IsTrue(pinnotexist);
        }


        public void DeleteCreatedPinByTeacher()
        {
           
            //Wait for loading pins at right panel
            var el2 = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(15)).Until(
                ExpectedConditions.ElementExists(By.ClassName("sk-homepage-news-links-block")));

            Assert.IsTrue(el2.GetAttribute("Class").Contains("sk-homepage-news-links-block"));

            //Open pop up menu for delete
            ReadOnlyCollection<IWebElement> list = _driverGC.FindElements(By.XPath("//a[contains(@data-info,'Selenium pin test')]"));
            list[0].Click();
            _driverGC.FindElement(By.XPath(".//*[@id='sk-pin-delete-link']/div/div")).Click();

            // Wait for pop-up "Delete" appear
            var deletebutton = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.ClassName("sk-button-white")));

            Assert.IsTrue(deletebutton.GetAttribute("class").Contains("sk-button-white"));

            //Press Delete
            _driverGC.FindElement(By.Id("sk-news-delete-confirmationButtonOkButtonId")).Click();


            //Wait for loading pins at right panel
            var el3 = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.ClassName("sk-homepage-news-links-block")));

            Assert.IsTrue(el3.GetAttribute("Class").Contains("sk-homepage-news-links-block"));

            // Check that pin not exist
            var pinnotexist = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//a[contains(@data-info,'Selenium pin test')]"))); 

            Assert.IsTrue(pinnotexist);
        }

        public void DeleteEditedPinByTeacher()
        {

            //Wait for loading pins at right panel
            var el2 = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.ClassName("sk-homepage-news-links-block")));

            Assert.IsTrue(el2.GetAttribute("Class").Contains("sk-homepage-news-links-block"));

            //Open pop up menu for delete
            ReadOnlyCollection<IWebElement> list = _driverGC.FindElements(By.XPath("//a[contains(@data-info,'Selenium pin test.Edited')]"));
            list[0].Click();
            _driverGC.FindElement(By.XPath(".//*[@id='sk-pin-delete-link']/div/div")).Click();

            // Wait for pop-up "Delete" appear
            var deletebutton = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.ClassName("sk-button-white")));

            Assert.IsTrue(deletebutton.GetAttribute("class").Contains("sk-button-white"));

            //Press Delete
            _driverGC.FindElement(By.Id("sk-news-delete-confirmationButtonOkButtonId")).Click();


            //Wait for loading pins at right panel
            var el3 = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.ClassName("sk-homepage-news-links-block")));

            Assert.IsTrue(el3.GetAttribute("Class").Contains("sk-homepage-news-links-block"));

            // Check that pin not exist
            var pinnotexist = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//a[contains(@data-info,'Selenium pin test.Edited')]")));

            Assert.IsTrue(pinnotexist);
        }

        public void TeacherNotCreatePin()
        {
            // Add text to iframe
            IWebElement frame = _driverGC.FindElement(By.ClassName("cke_wysiwyg_frame"));
            _driverGC.SwitchTo().Frame(frame);
            _driverGC.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            _driverGC.FindElement(By.XPath("/html/body")).Click();
            _driverGC.FindElement(By.XPath("html/body")).SendKeys("Selenium pin test");

            // Back from iframe 
            _driverGC.SwitchTo().Window(_driverGC.CurrentWindowHandle);

            // Save
            _driverGC.FindElement(By.XPath("//div[2]/div/div/input")).Click();

            //Check Feedback appear
            var feedback = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.XPath(".//*[@id='ErrorSummary_container']")));

            Assert.IsTrue(feedback.GetAttribute("class").Contains("ccl-feedback-error"));
        }
    }
}

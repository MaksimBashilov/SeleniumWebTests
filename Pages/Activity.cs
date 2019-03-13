using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;
using Web.UI.Controls.Common.Utils;
using System.Threading;


namespace Maksim.Web.SeleniumTests.Pages
{
    class Activity : BaseTest
    {
        private IWebDriver _driverGC;

        public Activity(IWebDriver driver)

        {
            _driverGC = driver;
            PageFactory.InitElements(_driverGC, this);

            // Go to Calendar Page
            _driverGC.Navigate().GoToUrl($"{TestHost}/staff/calendar/myCalendar");

            // Press on a button Create activity

            _driverGC.FindElement(By.CssSelector("#sk-calendar-toolbar > div.h-fl-r.h-mrt10.sk-toolbar-right-items-container > button.ccl-button.sk-button-blue.sk-font-icon.sk-button-icon-only.sk-button-style-add")).Click();

            // Wait for a page loading
            var activitititel = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.CssSelector("#select2-SelectedActivityTypeId-container")));

            Assert.IsTrue(activitititel.GetAttribute("Class").Contains("select2-selection__rendered"));

            //Wait for page loading
            _driverGC.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);

        }

        public void CreateActivity()
        {

            // Add title, description
            _driverGC.FindElement(By.Id("ActivityText")).SendKeys("Selenium Activity");
            _driverGC.FindElement(By.Id("Description")).SendKeys("Selenium Activity Description");

            // Choose class in drop down
            _driverGC.FindElement(By.Id("PhotoAlbumSearchTextboxInput")).SendKeys("00a");

            _driverGC.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);

            _driverGC.FindElement(By.CssSelector("#PhotoAlbumAudienceAutocomplete > div > div.nano.nano-constant-scroll.sk-recipients-multiselect-recipients-list.has-scrollbar > div.nano-content > div.sk-recipients-multiselect-recipients-list-option")).Click();

            //Scroll to the bottom 

            IWebElement element1 = _driverGC.FindElement(By.Id("PublishingSettings_AvailableSettings_0__IsChecked"));
            ((IJavaScriptExecutor)_driverGC).ExecuteScript("arguments[0].scrollIntoView(true);", element1);
            Thread.Sleep(500);

            //Checkboxes
            _driverGC.FindElement(By.CssSelector("#sk-publishingSettings-container > ol > li:nth-child(2) > div > div > ins")).Click();
            _driverGC.FindElement(By.CssSelector("#sk-publishingSettings-container > ol > li:nth-child(3) > div > div > ins")).Click();

            // Press OK 
            _driverGC.FindElement(By.Id("sk-editActivity-ok-or-cancel-buttonsOkButtonId")).Click();


            // Check feedback

            var feedback = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.CssSelector("#genericFeedbackBaseLayout_text")));

            Assert.IsTrue(feedback.GetAttribute("class").Contains(CommonCssClasses.Controls.Feedback.Text));

            // Check activity appear

            var activivtyexist = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.XPath("//span[text() = 'Selenium Activity']")));

            Assert.IsTrue(activivtyexist.GetAttribute("class").Contains("sk-event-title"));

        }

        public void CreateActivityWithConfirmation()
        {

            // Add title, description
            _driverGC.FindElement(By.Id("ActivityText")).SendKeys("Selenium Activity");
            _driverGC.FindElement(By.Id("Description")).SendKeys("Selenium Activity Description");

            // Choose class in drop down
            _driverGC.FindElement(By.Id("PhotoAlbumSearchTextboxInput")).SendKeys("00a");

            //Click on a any place
            _driverGC.FindElement(By.Id("Description")).Click();

            // Press OK 
            _driverGC.FindElement(By.Id("sk-editActivity-ok-or-cancel-buttonsOkButtonId")).Click();

            //Wait for ConfirmationMessage

            var confirmation = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementIsVisible(By.Id("PhotoAlbumEditNotCompletedSearchConfirmationDialog_inner")));

            Assert.IsTrue(confirmation.GetAttribute("class").Contains("ccl-cssmodal-modal-inner"));

            //Press Save
            _driverGC.FindElement(By.XPath("//*[starts-with(@id, 'OkOrCancel') and @class=\"ccl-button sk-button-blue sk-font-icon sk-button-text-only\"]")).Click();

            // Check feedback

            var feedback = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.CssSelector("#genericFeedbackBaseLayout_text")));

            Assert.IsTrue(feedback.GetAttribute("class").Contains(CommonCssClasses.Controls.Feedback.Text));

            // Check activity appear

            var activivtyexist = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.XPath("//span[text() = 'Selenium Activity']")));

            Assert.IsTrue(activivtyexist.GetAttribute("class").Contains("sk-event-title"));
        }

        public void CreateActivityNoClass()
        {

            // Add title, text, participants, checkboxes
            _driverGC.FindElement(By.Id("ActivityText")).SendKeys("Selenium Activity");
            _driverGC.FindElement(By.Id("Description")).SendKeys("Selenium Activity Description");

            //Scroll to the bottom 

            IWebElement element1 = _driverGC.FindElement(By.Id("PublishingSettings_AvailableSettings_0__IsChecked"));
            ((IJavaScriptExecutor)_driverGC).ExecuteScript("arguments[0].scrollIntoView(true);", element1);
            Thread.Sleep(500);

            //Checkboxes
            _driverGC.FindElement(By.CssSelector("#sk-publishingSettings-container > ol > li:nth-child(2) > div > div > ins")).Click();
            _driverGC.FindElement(By.CssSelector("#sk-publishingSettings-container > ol > li:nth-child(3) > div > div > ins")).Click();

            // Press OK 
            _driverGC.FindElement(By.Id("sk-editActivity-ok-or-cancel-buttonsOkButtonId")).Click();

            // Check error feedback

            var feedback = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.Id("ErrorSummary_text")));

            Assert.IsTrue(feedback.GetAttribute("class").Contains("ccl-feedback-text"));
        }

        public void EditActivityNoUndo()
        {

            // Add title, description
            _driverGC.FindElement(By.Id("ActivityText")).SendKeys("Selenium Activity.ForEdit");
            _driverGC.FindElement(By.Id("Description")).SendKeys("Selenium Activity Description.ForEdit");

            // Choose class in drop down
            _driverGC.FindElement(By.Id("PhotoAlbumSearchTextboxInput")).SendKeys("00a");

            _driverGC.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);

            _driverGC.FindElement(By.CssSelector("#PhotoAlbumAudienceAutocomplete > div > div.nano.nano-constant-scroll.sk-recipients-multiselect-recipients-list.has-scrollbar > div.nano-content > div.sk-recipients-multiselect-recipients-list-option")).Click();

            //Scroll to the bottom 

            IWebElement element1 = _driverGC.FindElement(By.Id("PublishingSettings_AvailableSettings_0__IsChecked"));
            ((IJavaScriptExecutor)_driverGC).ExecuteScript("arguments[0].scrollIntoView(true);", element1);
            Thread.Sleep(500);

            //Checkboxes
            _driverGC.FindElement(By.CssSelector("#sk-publishingSettings-container > ol > li:nth-child(2) > div > div > ins")).Click();
            _driverGC.FindElement(By.CssSelector("#sk-publishingSettings-container > ol > li:nth-child(3) > div > div > ins")).Click();

            // Press OK 
            _driverGC.FindElement(By.Id("sk-editActivity-ok-or-cancel-buttonsOkButtonId")).Click();


            // Check feedback

            var feedback = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.CssSelector("#genericFeedbackBaseLayout_text")));

            Assert.IsTrue(feedback.GetAttribute("class").Contains(CommonCssClasses.Controls.Feedback.Text));

            // Check activity appear

            var activivtyexist = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.XPath("//span[text() = 'Selenium Activity.ForEdit']")));

            Assert.IsTrue(activivtyexist.GetAttribute("class").Contains("sk-event-title"));

            //Go to activivty page
            _driverGC.Navigate()
                .GoToUrl(
                    $"{TestHost}/staff/calendar/activities");

            //Open drop down
            _driverGC.FindElement(By.XPath("//B[text()='Selenium Activity.ForEdit']/../../..")).Click();

            //Wait for a visisbility of a button and click on it
            Thread.Sleep(1000);

            //Press button Edit
            _driverGC.FindElement(By.XPath("//div[@class=\"sk-activity-body in collapse\" and .//b[contains(.,'Selenium Activity Description.ForEdit')]]//a[contains(@href,'/staff/calendar/activities/Edit')]")).Click();

            // Wait for a page loading
            var activitititel1 = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
            ExpectedConditions.ElementExists(By.Id("ActivityText")));

            Assert.IsTrue(activitititel1.GetAttribute("Class").Contains("sk-calendar-textinput"));

            // Add title, description
            _driverGC.FindElement(By.Id("ActivityText")).SendKeys("Edited.");


            // Press OK 
            _driverGC.FindElement(By.Id("sk-editActivity-ok-or-cancel-buttonsOkButtonId")).Click();


            // Check feedback

            var feedback1 = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.CssSelector("#genericFeedbackBaseLayout_text")));

            Assert.IsTrue(feedback1.GetAttribute("class").Contains(CommonCssClasses.Controls.Feedback.Text));

            // Check edited activity appear

            var activivtyexist2 = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.XPath("//B[text()='Edited.Selenium Activity.ForEdit']/../.")));

            Assert.IsTrue(activivtyexist2.GetAttribute("title").Contains("Edited.Selenium Activity.ForEdit"));
        }

        public void EditActivityWithUndo()
        {

            // Add title, description
            _driverGC.FindElement(By.Id("ActivityText")).SendKeys("Selenium Activity.ForUndo");
            _driverGC.FindElement(By.Id("Description")).SendKeys("Selenium Activity Description.ForUndo");

            // Choose class in drop down
            _driverGC.FindElement(By.Id("PhotoAlbumSearchTextboxInput")).SendKeys("00a");

            _driverGC.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);

            _driverGC.FindElement(By.CssSelector("#PhotoAlbumAudienceAutocomplete > div > div.nano.nano-constant-scroll.sk-recipients-multiselect-recipients-list.has-scrollbar > div.nano-content > div.sk-recipients-multiselect-recipients-list-option")).Click();

            //Scroll to the bottom 

            IWebElement element1 = _driverGC.FindElement(By.Id("PublishingSettings_AvailableSettings_0__IsChecked"));
            ((IJavaScriptExecutor)_driverGC).ExecuteScript("arguments[0].scrollIntoView(true);", element1);
            Thread.Sleep(500);

            //Checkboxes
            _driverGC.FindElement(By.CssSelector("#sk-publishingSettings-container > ol > li:nth-child(2) > div > div > ins")).Click();
            _driverGC.FindElement(By.CssSelector("#sk-publishingSettings-container > ol > li:nth-child(3) > div > div > ins")).Click();

            // Press OK 
            _driverGC.FindElement(By.Id("sk-editActivity-ok-or-cancel-buttonsOkButtonId")).Click();


            // Check feedback

            var feedback = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.CssSelector("#genericFeedbackBaseLayout_text")));

            Assert.IsTrue(feedback.GetAttribute("class").Contains(CommonCssClasses.Controls.Feedback.Text));

            // Check activity appear

            var activivtyexist = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.XPath("//span[text() = 'Selenium Activity.ForUndo']")));

            Assert.IsTrue(activivtyexist.GetAttribute("class").Contains("sk-event-title"));

            //Go to activivty page
            _driverGC.Navigate()
                .GoToUrl(
                    $"{TestHost}/staff/calendar/activities");

            //Open drop down
            _driverGC.FindElement(By.XPath("//B[text()='Selenium Activity.ForUndo']/../../..")).Click();

            //Wait for a visibility of a button and click on it
            Thread.Sleep(1000);

            //Press button Edit
            _driverGC.FindElement(By.XPath("//div[@class=\"sk-activity-body in collapse\" and .//b[contains(.,'Selenium Activity Description.ForUndo')]]//a[contains(@href,'/staff/calendar/activities/Edit')]")).Click();

            // Wait for a page loading
            var activitititel1 = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
            ExpectedConditions.ElementExists(By.Id("ActivityText")));

            Assert.IsTrue(activitititel1.GetAttribute("Class").Contains("sk-calendar-textinput"));

            // Add title, description
            _driverGC.FindElement(By.Id("ActivityText")).SendKeys("Edited.");


            // Press OK 
            _driverGC.FindElement(By.Id("sk-editActivity-ok-or-cancel-buttonsOkButtonId")).Click();


            // Check feedback

            var feedback1 = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.CssSelector("#genericFeedbackBaseLayout_text")));

            Assert.IsTrue(feedback1.GetAttribute("class").Contains(CommonCssClasses.Controls.Feedback.Text));

            // Press Undo
            _driverGC.FindElement(By.Id("genericFeedbackBaseLayout_UL")).Click();

            // Check activity not appear

            var activityundo = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//B[text()='Edited.Selenium Activity.ForUndo']/../.")));

            Assert.IsTrue(activityundo);

            // Check edited activity appear, without undo

            var activivtyexist2 = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.XPath("//B[text()='Selenium Activity.ForUndo']/../.")));

            Assert.IsTrue(activivtyexist2.GetAttribute("title").Contains("Selenium Activity.ForUndo"));
        }

        public void DeleteActivityNoUndo()
        {

            // Add title, description
            _driverGC.FindElement(By.Id("ActivityText")).SendKeys("Selenium Activity.ForDelete");
            _driverGC.FindElement(By.Id("Description")).SendKeys("Selenium Activity Description.ForDelete");

            // Choose class in drop down
            _driverGC.FindElement(By.Id("PhotoAlbumSearchTextboxInput")).SendKeys("00a");

            _driverGC.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);

            _driverGC.FindElement(By.CssSelector("#PhotoAlbumAudienceAutocomplete > div > div.nano.nano-constant-scroll.sk-recipients-multiselect-recipients-list.has-scrollbar > div.nano-content > div.sk-recipients-multiselect-recipients-list-option")).Click();

            //Scroll to the bottom 

            IWebElement element1 = _driverGC.FindElement(By.Id("PublishingSettings_AvailableSettings_0__IsChecked"));
            ((IJavaScriptExecutor)_driverGC).ExecuteScript("arguments[0].scrollIntoView(true);", element1);
            Thread.Sleep(500);

            //Checkboxes
            _driverGC.FindElement(By.CssSelector("#sk-publishingSettings-container > ol > li:nth-child(2) > div > div > ins")).Click();
            _driverGC.FindElement(By.CssSelector("#sk-publishingSettings-container > ol > li:nth-child(3) > div > div > ins")).Click();

            // Press OK 
            _driverGC.FindElement(By.Id("sk-editActivity-ok-or-cancel-buttonsOkButtonId")).Click();


            // Check feedback

            var feedback = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.CssSelector("#genericFeedbackBaseLayout_text")));

            Assert.IsTrue(feedback.GetAttribute("class").Contains(CommonCssClasses.Controls.Feedback.Text));

            // Check activity appear

            var activivtyexist = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.XPath("//span[text() = 'Selenium Activity.ForDelete']")));

            Assert.IsTrue(activivtyexist.GetAttribute("class").Contains("sk-event-title"));

            //Go to activivty page
            _driverGC.Navigate()
                .GoToUrl(
                    $"{TestHost}/staff/calendar/activities");

            //Open drop down
            _driverGC.FindElement(By.XPath("//B[text()='Selenium Activity.ForDelete']/../../..")).Click();

            //Wait for a visisbility of a button and click on it
            Thread.Sleep(1000);

            //Press button Delete
            _driverGC.FindElement(By.XPath("//div[@class=\"sk-activity-body in collapse\" and .//b[contains(.,'Selenium Activity Description.ForDelete')]]//a[contains(@href,'/staff/calendar/activities/Delete')]")).Click();

            // Wait for a page loading
            var activitititel1 = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.ClassName("ccl-formpanel-descriptionlabel")));

            Assert.IsTrue(activitititel1.GetAttribute("Class").Contains("ccl-formpanel-descriptionlabel"));

            //Press on a button delete
            _driverGC.FindElement(By.XPath("//*[starts-with(@id, 'OkOrCancel') and @class=\"ccl-button sk-button-blue sk-font-icon sk-button-text-only\"]")).Click();



            // Check feedback

            var feedback1 = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.CssSelector("#genericFeedbackBaseLayout_text")));

            Assert.IsTrue(feedback1.GetAttribute("class").Contains(CommonCssClasses.Controls.Feedback.Text));

            // Check deleted activity appear

            var activivtydelete = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//B[text()='Selenium Activity.ForDelete']/../.")));

            Assert.IsTrue(activivtydelete);
        }


        public void DeleteCreatedActivityNoUndo()
        {

            //Go to activivty page
            _driverGC.Navigate()
                .GoToUrl(
                    $"{TestHost}/staff/calendar/activities");

            //Open drop down
            _driverGC.FindElement(By.XPath("//B[text()='Selenium Activity']/../../..")).Click();

            //Wait for a visisbility of a button and click on it
            Thread.Sleep(1000);

            //Press button Delete
            _driverGC.FindElement(By.XPath("//div[@class=\"sk-activity-body in collapse\" and .//b[contains(.,'Selenium Activity Description')]]//a[contains(@href,'/staff/calendar/activities/Delete')]")).Click();

            // Wait for a page loading
            var activitititel1 = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
            ExpectedConditions.ElementExists(By.ClassName("ccl-formpanel-descriptionlabel")));

            Assert.IsTrue(activitititel1.GetAttribute("Class").Contains("ccl-formpanel-descriptionlabel"));

            //Press on a button delete
            _driverGC.FindElement(By.XPath("//*[starts-with(@id, 'OkOrCancel') and @class=\"ccl-button sk-button-blue sk-font-icon sk-button-text-only\"]")).Click();


            // Check feedback

            var feedback1 = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.CssSelector("#genericFeedbackBaseLayout_text")));

            Assert.IsTrue(feedback1.GetAttribute("class").Contains(CommonCssClasses.Controls.Feedback.Text));

            // Check deleted activity appear

            var activivtydelete = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//B[text()='Selenium Activity']/../.")));

            Assert.IsTrue(activivtydelete);
        }


        public void DeleteEditedActivityNoUndo()
        {

            //Go to activivty page
            _driverGC.Navigate()
                .GoToUrl(
                    $"{TestHost}/staff/calendar/activities");

            //Open drop down
            _driverGC.FindElement(By.XPath("//B[text()='Edited.Selenium Activity.ForEdit']/../../..")).Click();

            //Wait for a visisbility of a button and click on it
            Thread.Sleep(1000);

            //Press button Delete
            _driverGC.FindElement(By.XPath("//div[@class=\"sk-activity-body in collapse\" and .//b[contains(.,'Selenium Activity Description.ForEdit')]]//a[contains(@href,'/staff/calendar/activities/Delete')]")).Click();

            // Wait for a page loading
            var activitititel1 = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.ClassName("ccl-formpanel-descriptionlabel")));

            Assert.IsTrue(activitititel1.GetAttribute("Class").Contains("ccl-formpanel-descriptionlabel"));

            //Press on a button delete
            _driverGC.FindElement(By.XPath("//*[starts-with(@id, 'OkOrCancel') and @class=\"ccl-button sk-button-blue sk-font-icon sk-button-text-only\"]")).Click();



            // Check feedback

            var feedback1 = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.CssSelector("#genericFeedbackBaseLayout_text")));

            Assert.IsTrue(feedback1.GetAttribute("class").Contains(CommonCssClasses.Controls.Feedback.Text));

            // Check deleted activity appear

            var activivtydelete = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//B[text()='Edited.Selenium Activity.ForEdit']/../.")));

            Assert.IsTrue(activivtydelete);
        }

        public void DeleteEditedUndoActivityNoUndo()
        {

            //Go to activivty page
            _driverGC.Navigate()
                .GoToUrl(
                    $"{TestHost}/staff/calendar/activities");

            //Open drop down
            _driverGC.FindElement(By.XPath("//B[text()='Selenium Activity.ForUndo']/../../..")).Click();

            //Wait for a visisbility of a button and click on it
            Thread.Sleep(1000);

            //Press button Delete
            _driverGC.FindElement(By.XPath("//div[@class=\"sk-activity-body in collapse\" and .//b[contains(.,'Selenium Activity Description.ForUndo')]]//a[contains(@href,'/staff/calendar/activities/Delete')]")).Click();
            
            // Wait for a page loading
            var activitititel1 = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.ClassName("ccl-formpanel-descriptionlabel")));

            Assert.IsTrue(activitititel1.GetAttribute("Class").Contains("ccl-formpanel-descriptionlabel"));

            //Press on a button delete
            _driverGC.FindElement(By.XPath("//*[starts-with(@id, 'OkOrCancel') and @class=\"ccl-button sk-button-blue sk-font-icon sk-button-text-only\"]")).Click();


            // Check feedback

            var feedback1 = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.CssSelector("#genericFeedbackBaseLayout_text")));

            Assert.IsTrue(feedback1.GetAttribute("class").Contains(CommonCssClasses.Controls.Feedback.Text));

            // Check deleted activity appear

            var activivtydelete = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//B[text()='Selenium Activity.ForUndo']/../.")));

            Assert.IsTrue(activivtydelete);
        }

        public void DeleteActivityWithUndo()
        {

            // Add title, description
            _driverGC.FindElement(By.Id("ActivityText")).SendKeys("Selenium Activity.ForDelete.Undo");
            _driverGC.FindElement(By.Id("Description")).SendKeys("Selenium Activity Description.ForDelete.Undo");

            // Choose class in drop down
            _driverGC.FindElement(By.Id("PhotoAlbumSearchTextboxInput")).SendKeys("00a");

            _driverGC.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);

            _driverGC.FindElement(By.CssSelector("#PhotoAlbumAudienceAutocomplete > div > div.nano.nano-constant-scroll.sk-recipients-multiselect-recipients-list.has-scrollbar > div.nano-content > div.sk-recipients-multiselect-recipients-list-option")).Click();

            //Scroll to the bottom 

            IWebElement element1 = _driverGC.FindElement(By.Id("PublishingSettings_AvailableSettings_0__IsChecked"));
            ((IJavaScriptExecutor)_driverGC).ExecuteScript("arguments[0].scrollIntoView(true);", element1);
            Thread.Sleep(500);

            //Checkboxes
            _driverGC.FindElement(By.CssSelector("#sk-publishingSettings-container > ol > li:nth-child(2) > div > div > ins")).Click();
            _driverGC.FindElement(By.CssSelector("#sk-publishingSettings-container > ol > li:nth-child(3) > div > div > ins")).Click();

            // Press OK 
            _driverGC.FindElement(By.Id("sk-editActivity-ok-or-cancel-buttonsOkButtonId")).Click();


            // Check feedback

            var feedback = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.CssSelector("#genericFeedbackBaseLayout_text")));

            Assert.IsTrue(feedback.GetAttribute("class").Contains(CommonCssClasses.Controls.Feedback.Text));

            // Check activity appear

            var activivtyexist = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.XPath("//span[text() = 'Selenium Activity.ForDelete.Undo']")));

            Assert.IsTrue(activivtyexist.GetAttribute("class").Contains("sk-event-title"));

            //Go to activivty page
            _driverGC.Navigate()
                .GoToUrl(
                    $"{TestHost}/staff/calendar/activities");

            //Open drop down
            _driverGC.FindElement(By.XPath("//B[text()='Selenium Activity.ForDelete.Undo']/../../..")).Click();

            //Wait for a visisbility of a button and click on it
            Thread.Sleep(1000);

            //Press button Delete
            _driverGC.FindElement(By.XPath("//div[@class=\"sk-activity-body in collapse\" and .//b[contains(.,'Selenium Activity Description.ForDelete.Undo')]]//a[contains(@href,'/staff/calendar/activities/Delete')]")).Click();

            // Wait for a page loading
            var activitititel1 = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.ClassName("ccl-formpanel-descriptionlabel")));

            Assert.IsTrue(activitititel1.GetAttribute("Class").Contains("ccl-formpanel-descriptionlabel"));

            //Press on a button delete
            _driverGC.FindElement(By.XPath("//*[starts-with(@id, 'OkOrCancel') and @class=\"ccl-button sk-button-blue sk-font-icon sk-button-text-only\"]")).Click();


            // Check feedback

            var feedback1 = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.CssSelector("#genericFeedbackBaseLayout_text")));

            Assert.IsTrue(feedback1.GetAttribute("class").Contains(CommonCssClasses.Controls.Feedback.Text));

            // Press Undo
            _driverGC.FindElement(By.Id("genericFeedbackBaseLayout_UL")).Click();

            // Check activity not deleted

            var activivtynotdeleted = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.XPath("//B[text()='Selenium Activity.ForDelete.Undo']/../.")));

            Assert.IsTrue(activivtynotdeleted.GetAttribute("title").Contains("Selenium Activity.ForDelete.Undo"));

        }


        public void DeleteDeletedUndoActivity()
        {

            //Go to activivty page
            _driverGC.Navigate()
                .GoToUrl(
                    $"{TestHost}/staff/calendar/activities");

            //Open drop down
            _driverGC.FindElement(By.XPath("//B[text()='Selenium Activity.ForDelete.Undo']/../../..")).Click();

            //Wait for a visisbility of a button and click on it
            Thread.Sleep(1000);

            //Press button Delete
            _driverGC.FindElement(By.XPath("//div[@class=\"sk-activity-body in collapse\" and .//b[contains(.,'Selenium Activity Description.ForDelete.Undo')]]//a[contains(@href,'/staff/calendar/activities/Delete')]")).Click();
            
            // Wait for a page loading
            var activitititel1 = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.ClassName("ccl-formpanel-descriptionlabel")));

            Assert.IsTrue(activitititel1.GetAttribute("Class").Contains("ccl-formpanel-descriptionlabel"));

            //Press on a button delete
            _driverGC.FindElement(By.XPath("//*[starts-with(@id, 'OkOrCancel') and @class=\"ccl-button sk-button-blue sk-font-icon sk-button-text-only\"]")).Click();



            // Check feedback

            var feedback1 = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.CssSelector("#genericFeedbackBaseLayout_text")));

            Assert.IsTrue(feedback1.GetAttribute("class").Contains(CommonCssClasses.Controls.Feedback.Text));

            // Check deleted activity appear

            var activivtydelete = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//B[text()='Selenium Activity.ForDelete.Undo']/../.")));

            Assert.IsTrue(activivtydelete);
        }
    }

}
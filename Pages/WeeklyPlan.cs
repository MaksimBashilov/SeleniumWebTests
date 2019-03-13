using System;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using Web.UI.Controls.Common.Utils;
using NUnit.Framework;

namespace Maksim.Web.SeleniumTests.Pages
{
    class WeeklyPlan : BaseTest
    {
        private IWebDriver _driver;

        #region Elements


        [FindsBy(How = How.Id, Using = "ctl00_ContentAreaIframe")]
        private IWebElement Frame { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#sk-membership-dropdown-client-id")]
        private IWebElement СlassSelector { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#sk-weeks-dropdown-client-id")]
        private IWebElement WeekSelector { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#root > div > h3")]
        private IWebElement WeekPlanTitle { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#sk-print-plan-button-client-id")]
        private IWebElement PrintButon { get; set; }


        //publish
        [FindsBy(How = How.CssSelector, Using = "#sk-publish-plan-button-client-id")]
        private IWebElement PublishButton { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#sk-publish-week-plan-dialog_content > div > div > div:nth-child(1) > div > ins")]
        private IWebElement StudentIntraCheckbox { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#sk-publish-week-plan-dialog_content > div > div > div:nth-child(2) > div > ins")]
        private IWebElement ParentIntraCheckbox { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#sk-publish-week-plan-dialog_content > div > div > div:nth-child(3) > div > ins")]
        private IWebElement SkoleportenCheckbox { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#PublishPlanDialogOkOrCancelClientInstanceNameOkButtonId")]
        private IWebElement PublishButonPopUpUdgiv { get; set; }

        [FindsBy(How = How.CssSelector,
            Using = "#root > div > div.h-no-print.sk-weekly-plan-publishing-settings > span:nth-child(2)")]
        private IWebElement PublishText { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#sk-publish-week-plan-dialog_header")]
        private IWebElement PublishPlanTitle { get; set; }



        //DocumentBased plan
        [FindsBy(How = How.CssSelector,
            Using = "#root > div > div:nth-child(2) > div.sk-weekly-plan-content-cell > div > a")]
        private IWebElement TilfojFagGeneral { get; set; }

        [FindsBy(How = How.CssSelector,
            Using = "#root > div > div:nth-child(3) > div.sk-weekly-plan-content-cell > div > a")]
        private IWebElement TilfojFagMonday { get; set; }

        [FindsBy(How = How.CssSelector,
            Using = "#root > div > div:nth-child(4) > div.sk-weekly-plan-content-cell > div > a")]
        private IWebElement TilfojFagTuesday { get; set; }

        [FindsBy(How = How.CssSelector,
            Using = "#root > div > div:nth-child(5) > div.sk-weekly-plan-content-cell > div > a")]
        private IWebElement TilfojFagWednesday { get; set; }

        [FindsBy(How = How.CssSelector,
            Using = "#root > div > div:nth-child(6) > div.sk-weekly-plan-content-cell > div > a")]
        private IWebElement TilfojFagThursday { get; set; }

        [FindsBy(How = How.CssSelector,
            Using = "#root > div > div:nth-child(7) > div.sk-weekly-plan-content-cell > div > a")]
        private IWebElement TilfojFagFriday { get; set; }

        [FindsBy(How = How.CssSelector,
            Using = "#root > div > div:nth-child(3) > div.sk-weekly-plan-content-cell > div:nth-child(1) > div > span")]
        private IWebElement DocumentPlanCreatedTitle { get; set; }

        //CreatePlan Popup
        [FindsBy(How = How.CssSelector, Using = "#sk-add-or-edit-dialog_header")]
        private IWebElement PlanHeader { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#lesson_plan_editor-theme-selector")]
        private IWebElement CourseSelector { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#AddOrEditDialogOkOrCancelClientInstanceNameOkButtonId")]
        private IWebElement OkButtonPlan { get; set; }

        //Edit pop up
        [FindsBy(How = How.CssSelector,
            Using = "#root > div > div:nth-child(3) > div.sk-weekly-plan-content-cell > div:nth-child(1) > div > a")]
        private IWebElement RedigerGeneral { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#sk-remove-button")]
        private IWebElement SletButon { get; set; }


        [FindsBy(How = How.CssSelector, Using = "#root > div > div.sk-white-box.sk-weekly-plan-attachments")]
        private IWebElement Attachments { get; set; }

        //TemplateBasedPlan
        [FindsBy(How = How.CssSelector, Using = "#sk-create-plan-button-client-id")]
        private IWebElement TilfojTemplate { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#sk-edit-plan-button-client-id")]
        private IWebElement RedigerTemplate { get; set; }

        #endregion

        public WeeklyPlan(IWebDriver driver)
        {
            _driver = driver;
            PageFactory.InitElements(_driver, this);

            new WebDriverWait(_driver, TimeSpan.FromSeconds(30)).Until(
                ExpectedConditions.ElementIsVisible(By.Id("ctl00_PersonalResponsiveMenu_PersonalMenuNavigation")));

            // Go to Calendar Page
            _driver.Navigate().GoToUrl($"{TestHost}/main.aspx?TextUrl={TestUrl}%2Fstaff%2Fhomework%2Fweeklyplans%3FFromWeb%3Dtrue");


            //Wait for page loading
            _driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);

        }

        public void CreateDocumentPlan(string classname, string weekplannotetext, string week)
        {
            _driver.SwitchTo().Frame(Frame);

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            SelectElement dropdownclass = new SelectElement(СlassSelector);
            dropdownclass.SelectByText(classname);

            SelectElement dropdownweek = new SelectElement(WeekSelector);
            dropdownweek.SelectByText(week);

            TilfojFagGeneral.Click();

            new WebDriverWait(_driver, TimeSpan.FromSeconds(10)).Until(ElementIsVisible(PlanHeader));

            // Add text to iframe
            IWebElement frame = _driver.FindElement(By.ClassName("cke_wysiwyg_frame"));
            _driver.SwitchTo().Frame(frame);
            ;
            _driver.FindElement(By.XPath("/html/body")).Click();
            _driver.FindElement(By.XPath("html/body")).SendKeys(weekplannotetext);

            // Back from iframe and save
            _driver.SwitchTo().Window(_driver.CurrentWindowHandle);
            _driver.SwitchTo().Frame(Frame);

            OkButtonPlan.Click();

            //Check that plan is created
            var noteCreated = new WebDriverWait(_driver, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.XPath("//p[contains(text(),'Selenium Weekly Plan')]")));
            Assert.IsTrue(noteCreated.Displayed);

            //Check Feedback appear
            var feedback = new WebDriverWait(_driver, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.XPath(".//*[@id='genericFeedbackBaseLayout_text']")));
            Assert.IsTrue(feedback.GetAttribute("class").Contains(CommonCssClasses.Controls.Feedback.Text));

            _driver.SwitchTo().Window(_driver.CurrentWindowHandle);

            _driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);
        }

        public void EditDocumentPlan(string weekplannotetextedited)
        {
            _driver.SwitchTo().Frame(Frame);

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            RedigerGeneral.Click();

            new WebDriverWait(_driver, TimeSpan.FromSeconds(10)).Until(ElementIsVisible(PlanHeader));

            // Add text to iframe
            IWebElement frame = _driver.FindElement(By.ClassName("cke_wysiwyg_frame"));
            _driver.SwitchTo().Frame(frame);
            ;
            _driver.FindElement(By.XPath("/html/body")).Click();
            _driver.FindElement(By.XPath("html/body")).SendKeys(weekplannotetextedited);

            // Back from iframe and save
            _driver.SwitchTo().Window(_driver.CurrentWindowHandle);
            _driver.SwitchTo().Frame(Frame);

            OkButtonPlan.Click();
            
            //Weekly plan is edited and shown on a page
            var noteCreated = new WebDriverWait(_driver, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.XPath("//p[contains(text(),'Selenium Weekly Plan.Edited')]")));
            Assert.IsTrue(noteCreated.Displayed);

            //Check Feedback appear
            var feedback = new WebDriverWait(_driver, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.XPath(".//*[@id='genericFeedbackBaseLayout_text']")));
            Assert.IsTrue(feedback.GetAttribute("class").Contains(CommonCssClasses.Controls.Feedback.Text));

            _driver.SwitchTo().Window(_driver.CurrentWindowHandle);

            _driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);
        }

        public void DeleteDocumentPlan()
        {
            _driver.SwitchTo().Frame(Frame);

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            RedigerGeneral.Click();

            new WebDriverWait(_driver, TimeSpan.FromSeconds(10)).Until(ElementIsVisible(PlanHeader));

            SletButon.Click();

            //Check Feedback appear
            var feedback = new WebDriverWait(_driver, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.XPath(".//*[@id='genericFeedbackBaseLayout_text']")));
            Assert.IsTrue(feedback.GetAttribute("class").Contains(CommonCssClasses.Controls.Feedback.Text));
           
            //Check  note is edited
            var noteDeleted = new WebDriverWait(_driver, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//p[contains(text(),'Selenium Weekly Plan')]")));
            Assert.IsTrue(noteDeleted);

            _driver.SwitchTo().Window(_driver.CurrentWindowHandle);

            _driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);
        }

        public void PublishDocumentPlan()
        {
            _driver.SwitchTo().Frame(Frame);

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            var NoPublish = new WebDriverWait(_driver, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.XPath("//span[contains(text(),'Ikke udgivet')]")));
            Assert.IsTrue(NoPublish.Displayed);

            PublishButton.Click();
            new WebDriverWait(_driver, TimeSpan.FromSeconds(10)).Until(ElementIsVisible(PublishPlanTitle));
            
            StudentIntraCheckbox.Click();
            ParentIntraCheckbox.Click();
            SkoleportenCheckbox.Click();

            PublishButonPopUpUdgiv.Click();

            var Publish = new WebDriverWait(_driver, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.XPath("//span[contains(text(),'Udgivet i ElevIntra, ForældreIntra og Skoleporten')]")));
            Assert.IsTrue(Publish.Displayed);

            //Check Feedback appear
            var feedback = new WebDriverWait(_driver, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.XPath(".//*[@id='genericFeedbackBaseLayout_text']")));
            Assert.IsTrue(feedback.GetAttribute("class").Contains(CommonCssClasses.Controls.Feedback.Text));


            _driver.SwitchTo().Window(_driver.CurrentWindowHandle);

            _driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);
        }

        public void OpenTemplatePlan(string classname, string week)
        {
            _driver.SwitchTo().Frame(Frame);

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            SelectElement dropdownclass = new SelectElement(СlassSelector);
            dropdownclass.SelectByText(classname);

            SelectElement dropdownweek = new SelectElement(WeekSelector);
            dropdownweek.SelectByText(week);

            TilfojTemplate.Click();

            _driver.SwitchTo().Window(_driver.WindowHandles.LastOrDefault());

            //Check  note is edited
            var nsipage = new WebDriverWait(_driver, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//h3[contains(text(),'Ugeplan for 1A - uge 40-2018')]")));
            Assert.IsTrue(nsipage);

            var csipage = new WebDriverWait(_driver, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.XPath("//b[contains(text(),'Opret ugeplan')]")));
            Assert.IsTrue(csipage.Displayed);


            _driver.SwitchTo().Window(_driver.WindowHandles.First());

            _driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);
        }
    }
}

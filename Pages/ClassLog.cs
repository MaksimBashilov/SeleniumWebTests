using System;
using System.Threading;
using Maksim.Web.SeleniumTests.Resources;
using Maksim.SeleniumTests;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using Web.UI.Controls.Common.Utils;
using NUnit.Framework;

namespace Maksim.Web.SeleniumTests.Pages
{
    class ClassLog : BaseTest
    {
        private IWebDriver _driver;

        #region Elements

        [FindsBy(How = How.Id, Using = "passwordToValidate")]
        private IWebElement CodeInputField { get; set; }

        [FindsBy(How = How.Id, Using = "ctl00_ContentAreaIframe")]
        private IWebElement Frame { get; set; }

        [FindsBy(How = How.CssSelector, Using = "body > div.sk-l-main-contaier > div > div.sk-l-content-full-width > div.sk-classlog-content-container > div.sk-classlog-toolbar-container > a")]
        private IWebElement CreateButton { get; set; }

        //Create pop up
        [FindsBy(How = How.CssSelector, Using = "#RecipientsAutocompleteViewModel_SearchTextInputClientId")]
        private IWebElement RecepientField { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#note-text")]
        private IWebElement NoteField { get; set; }
      
        //atachments
        [FindsBy(How = How.CssSelector, Using = "#add-edit-note-dialog_content > div:nth-child(1) > div > div:nth-child(3) > div.sk-add-attachments-container > a")]
        private IWebElement TilfojBilagIcon { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#fileUploader_input")]
        private IWebElement TilfojFiler { get; set; }
        
        [FindsBy(How = How.CssSelector, Using = "#CssModal_footer > div:nth-child(4) > button.ccl-button.sk-button-blue.sk-font-icon.sk-button-text-only")]
        private IWebElement Tilfoj { get; set; }

        [FindsBy(How = How.Id, Using = "SeleniumJpg.jpg")]
        private IWebElement GreenAttachPlate { get; set; }

        //Category selector
        [FindsBy(How = How.CssSelector, Using = "#add-edit-note-dialog_content > div:nth-child(1) > div > div:nth-child(4) > div.sk-extensible-options-list > div.sk-eol-input")]
        private IWebElement CategoryDropDown { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#add-edit-note-dialog_content > div:nth-child(1) > div > div:nth-child(4) > div.sk-extensible-options-list > div.sk-eol-dropdown-list > div > button")]
        private IWebElement ButtonNewCategory { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#add-edit-note-dialog_content > div:nth-child(1) > div > div:nth-child(4) > div.sk-extensible-options-list > div.sk-eol-dropdown-list > div > div > input[type=\"text\"]")]
        private IWebElement Categoryinputfield { get; set; }

        //checkbox
        [FindsBy(How = How.CssSelector, Using = "#add-edit-note-dialog_content > div:nth-child(1) > div > div:nth-child(5) > div")]
        private IWebElement CheckboxContactBook { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#MessageToAutocompleteViewModel_SearchTextInputClientId")]
        private IWebElement SendMessaOkgeField { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#CssModal_footer > div:nth-child(1) > input")]
        private IWebElement OkButton { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#CssModal_footer > div:nth-child(1) > a")]
        private IWebElement CancelButton { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[@class='sk-recipients-multiselect-recipients-list-option']")]
        private IWebElement FieldToMultiselect { get; set; }

        [FindsBy(How = How.ClassName, Using = "sk-classlog-groupslist")]
        private IWebElement LeftMenu { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#RecipientsAutocompleteViewModel_ContainerClientId > a")]
        private IWebElement PlusButon { get; set; }
       
        //unilogin menu
        [FindsBy(How = How.Id, Using = "user")]
        private IWebElement UniLoginLogin { get; set; }

        [FindsBy(How = How.Id, Using = "pass")]
        private IWebElement UniLoginPassword { get; set; }

        //three dots menu
        [FindsBy(How = How.Id, Using = "sk-context-menu-container")]
        private IWebElement ThreeDotsContainer { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[contains(text(),'Selenium class log note text')]//..//..//div[position()=5]//a")]
        private IWebElement ThreeDotsMenu { get; set; }

        [FindsBy(How = How.ClassName, Using = "sk-context-menu-blue-item")]
        private IWebElement Edit { get; set; }

        [FindsBy(How = How.ClassName, Using = "sk-context-menu-red-item")]
        private IWebElement Delete { get; set; }


        [FindsBy(How = How.XPath, Using = "//*[@id=\"sk-classlog-deletenote-dialogButtonOkButtonId\" and @class=\"ccl-button sk-button-red sk-font-icon sk-button-text-only\"]")]
        private IWebElement DeleteConfirm { get; set; }

        #endregion

        public ClassLog(IWebDriver driver)
        {
            _driver = driver;
            PageFactory.InitElements(_driver, this);

            new WebDriverWait(_driver, TimeSpan.FromSeconds(30)).Until(
                ExpectedConditions.ElementIsVisible(By.Id("ctl00_PersonalResponsiveMenu_PersonalMenuNavigation")));

            // Go to Calendar Page
            _driver.Navigate().GoToUrl($"{TestHost}/main.aspx?TextUrl={TestUrl}%2Fstaff%2Fclasslog%2Findex%3FFromWeb%3Dtrue");
            

            //Wait for page loading
            _driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);

        }


        public void AddNativeCode(string subject)
        {
            _driver.SwitchTo().Frame(Frame);
            CodeInputField.SendKeys(subject);
            CodeInputField.SendKeys(Keys.Enter);

            //Wait for page loading
            _driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);

            new WebDriverWait(_driver, TimeSpan.FromSeconds(10)).Until(ElementIsVisible(CreateButton));
        }

        public void AddUniLogineCode(IWebDriver driver, string login, string password)
        {
            _driver.SwitchTo().Frame(Frame);

            _driver.SwitchTo().Frame(driver.FindElement(By.CssSelector("iframe[title='Brug UNI-Login']")));

            new WebDriverWait(_driver, TimeSpan.FromSeconds(10)).Until(ExpectedConditions.ElementIsVisible(By.Id("outside")));

            // Add login and password
            UniLoginLogin.SendKeys(login);
            UniLoginPassword.SendKeys(password);
            UniLoginPassword.SendKeys(Keys.Enter);

            //Check that you don't see password field
            var el1 = new WebDriverWait(_driver, TimeSpan.FromSeconds(10)).Until(ExpectedConditions.InvisibilityOfElementWithText(By.Id("login"), "login"));

            Assert.IsTrue(el1);

            //Wait for page loading
            _driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);

            new WebDriverWait(_driver, TimeSpan.FromSeconds(10)).Until(ElementIsVisible(LeftMenu));

        }

        public void CreateNote(UserEntity to, string studentname, string note, string category)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            CreateButton.Click();

            new WebDriverWait(_driver, TimeSpan.FromSeconds(10)).Until(ElementIsVisible(PlusButon));

            RecepientField.SendKeys(studentname);
        
            new WebDriverWait(_driver, TimeSpan.FromSeconds(10)).Until(ElementIsVisible(FieldToMultiselect));

            IWebElement recepientList =
                driver.FindElement(By.XPath("//span[contains(@title,'" + to.DisplayName + "')]"));
            recepientList.Click();
            NoteField.SendKeys(note);

            CategoryDropDown.Click();

            ButtonNewCategory.Click();
            Categoryinputfield.SendKeys(category);
            Categoryinputfield.SendKeys(Keys.Enter);

            CheckboxContactBook.Click();

            OkButton.Click();

            //Check Feedback appear
            var feedback = new WebDriverWait(_driver, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.XPath(".//*[@id='genericFeedbackBaseLayout_text']")));
            Assert.IsTrue(feedback.GetAttribute("class").Contains(CommonCssClasses.Controls.Feedback.Text));

            _driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);
        }


        public void EditNote(string note)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            new WebDriverWait(_driver, TimeSpan.FromSeconds(10)).Until(ElementIsVisible(ThreeDotsMenu));

            ThreeDotsMenu.Click();
            new WebDriverWait(_driver, TimeSpan.FromSeconds(10)).Until(ElementIsVisible(ThreeDotsContainer));
          

            Edit.Click();

            new WebDriverWait(_driver, TimeSpan.FromSeconds(10)).Until(ElementIsVisible(TilfojBilagIcon));

            //NoteField.Clear();
            NoteField.SendKeys(note);
            Thread.Sleep(500);

            OkButton.Click();

            //Check Feedback appear
            var feedback = new WebDriverWait(_driver, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.XPath(".//*[@id='genericFeedbackBaseLayout_text']")));
            Assert.IsTrue(feedback.GetAttribute("class").Contains(CommonCssClasses.Controls.Feedback.Text));

            //Check  note is edited
            var noteEdited = new WebDriverWait(_driver, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.XPath("//div[contains(text(),'Selenium class log note text.Edited')]")));
            Assert.IsTrue(noteEdited.GetAttribute("class").Contains("sk-preserve-linebreaks"));

            _driver.SwitchTo().Window(_driver.CurrentWindowHandle);

            _driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);
        }


        public void DeleteNote()
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            new WebDriverWait(_driver, TimeSpan.FromSeconds(10)).Until(ElementIsVisible(ThreeDotsMenu));

            ThreeDotsMenu.Click();
            new WebDriverWait(_driver, TimeSpan.FromSeconds(10)).Until(ElementIsVisible(ThreeDotsContainer));


            Delete.Click();

            new WebDriverWait(_driver, TimeSpan.FromSeconds(10)).Until(ElementIsVisible(DeleteConfirm));
            DeleteConfirm.Click();


            //Check Feedback appear
            var feedback = new WebDriverWait(_driver, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.XPath(".//*[@id='genericFeedbackBaseLayout_text']")));
            Assert.IsTrue(feedback.GetAttribute("class").Contains(CommonCssClasses.Controls.Feedback.Text));

            //Check  note is edited
            var noteEdited = new WebDriverWait(_driver, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//div[contains(text(),'Selenium class log note text')]")));
            Assert.IsTrue(noteEdited);

            _driver.SwitchTo().Window(_driver.CurrentWindowHandle);

            _driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);
        }

        public void AddAttachmentsNote(UserEntity to, string studentname, string note, string category)
        {
            CreateButton.Click();

            new WebDriverWait(_driver, TimeSpan.FromSeconds(10)).Until(ElementIsVisible(PlusButon));
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            RecepientField.SendKeys(studentname);

            new WebDriverWait(_driver, TimeSpan.FromSeconds(10)).Until(ElementIsVisible(FieldToMultiselect));

            IWebElement recepientList =
                driver.FindElement(By.XPath("//span[contains(@title,'" + to.DisplayName + "')]"));
            recepientList.Click();
            NoteField.SendKeys(note);

            //Add Attachments
            TilfojBilagIcon.Click();
            new WebDriverWait(_driver, TimeSpan.FromSeconds(10)).Until(ElementIsVisible(Tilfoj));
            //AddAttachments.Click();

            //Upload file 
            TilfojFiler.SendKeys(ResourceManager.GenerateFullPath(ResourceManager.GetJpgFile()));

            // Wait for a file uploading
            var noprogressbar = new WebDriverWait(_driver, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.InvisibilityOfElementLocated(By.ClassName("progress")));

            Assert.IsTrue(noprogressbar);

            // Press on a button ok
            Tilfoj.Click();

            // Wait for a file show on a page
            // new WebDriverWait(_driver, TimeSpan.FromSeconds(10)).Until(ElementIsVisible(GreenAttachPlate));

            // Wait for pop up loaded
            var attachment = new WebDriverWait(_driver, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.ClassName("sk-nameplate-label")));

            Assert.IsTrue(attachment.GetAttribute("title").Contains("SeleniumJpg.jpg"));

            CategoryDropDown.Click();

            ButtonNewCategory.Click();
            Categoryinputfield.SendKeys(category);
            Categoryinputfield.SendKeys(Keys.Enter);

            CheckboxContactBook.Click();

            OkButton.Click();

            _driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);

            //Check Feedback appear
            var feedback = new WebDriverWait(_driver, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.XPath(".//*[@id='genericFeedbackBaseLayout_text']")));
            Assert.IsTrue(feedback.GetAttribute("class").Contains(CommonCssClasses.Controls.Feedback.Text));

            //Check atachmets appear
            var attachforside = new WebDriverWait(_driver, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.XPath("//a[contains(text(),'SeleniumJpg.jpg')]")));
            Assert.IsTrue(attachforside.Displayed);

            _driver.SwitchTo().Window(_driver.CurrentWindowHandle);
        }
    }

}

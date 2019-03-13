using System;
using Maksim.SeleniumTests;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using Web.UI.Controls.Common.Utils;
using NUnit.Framework;

namespace Maksim.Web.SeleniumTests.Pages
{
    public enum Tabs
    {
        Conversations,
        //Inbox,    Inbox has been removed recently
        Outbox,
        Draft,
        Unread,
        Archive
    }

    class Messages : BaseTest
    {
        private IWebDriver _driver;

        [FindsBy(How = How.XPath, Using = "//a[contains(@href,'create/message')]")]
        private IWebElement NewMessageButton { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@class='sk-searchsubmit-input']")]
        private IWebElement SearchForMessages { get; set; }

        [FindsBy(How = How.XPath, Using = "//li[@id='conversations']/h2")]
        private IWebElement Conversations { get; set; }

        [FindsBy(How = How.XPath, Using = "//li[@id='inbox']/h2")]
        private IWebElement Inbox { get; set; }

        [FindsBy(How = How.XPath, Using = "//li[@id='outbox']/h2")]
        private IWebElement Outbox { get; set; }

        [FindsBy(How = How.XPath, Using = "//li[@id='draft']/h2")]
        private IWebElement Draft { get; set; }

        [FindsBy(How = How.XPath, Using = "//li[@id='unread']/h2")]
        private IWebElement Unread { get; set; }

        [FindsBy(How = How.XPath, Using = "//li[@id='archive']/h2")]
        private IWebElement Archive { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@id='recipientInputClientId_mainMessageSelectorType']")]
        private IWebElement FieldTo { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[@class='sk-recipients-multiselect-recipients-list-option']")]
        private IWebElement FieldToMultiselect { get; set; }

        [FindsBy(How = How.XPath, Using = "//a[@data-toggle='collapse']")]
        private IWebElement AddCcBcc { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@id='recipientInputClientId_ccMessageSelectorType']")]
        private IWebElement FieldCc { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@id='recipientInputClientId_bccMessageSelectorType']")]
        private IWebElement FieldBcc { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@id='ComposeMessage_Subject']")]
        private IWebElement FieldSubject { get; set; }

        [FindsBy(How = How.XPath, Using = "//iframe[@class='cke_wysiwyg_frame cke_reset']")]
        private IWebElement CKEditorFrame { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@data-action-type='Send']")]
        private IWebElement ButtonSend { get; set; }

        [FindsBy(How = How.CssSelector, Using = "body > div.sk-l-main-contaier > div.sk-l-content-wrapper > div.sk-l-content-full-width > nav.sk-toolbar-container.h-mrb15.sk-messages-conversations-toolbar > div.h-fl-l.h-mrt10.sk-toolbar-left-items-container > button.ccl-button.sk-button-white.sk-font-icon.sk-button-icon-only.sk-icon-archive.sk-messages-conversations-barch-archive")]
        private IWebElement ButtonArchiveTop { get; set; }

        [FindsBy(How = How.Id, Using = "sk-messageconversations-delete-button-desktop")]
        private IWebElement ButtonDeleteTop { get; set; }

        [FindsBy(How = How.Id, Using = "sk-messages-toolbar-markas-multipicker")]
        private IWebElement DropDownMenuTop { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#toolbar-multipicker-label-menu-button > ul > li:nth-child(1) > a")]
        private IWebElement DropDownReadTop { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#toolbar-multipicker-label-menu-button > ul > li:nth-child(2) > a")]
        private IWebElement DropDownUnreadTop { get; set; }

        [FindsBy(How = How.CssSelector, Using = "body > div.sk-l-main-contaier > div.sk-l-content-wrapper > div.sk-l-content-full-width > div:nth-child(4) > div > div.sk-messages-conversations-right-pane.sk-messages-conversations-active-pane > div.nano.has-scrollbar > div.nano-content > div.sk-message-conversation-thread > div > div.sk-message-item > div > div.sk-message-archive-line > a")]
        private IWebElement ButtonBodyGemIArchive { get; set; }

        [FindsBy(How = How.Id, Using = "sk-messageconversations-reply-button")]
        private IWebElement ButtonReply { get; set; }

        [FindsBy(How = How.Id, Using = "sk-messageconversations-forward-button")]
        private IWebElement ButtonForward { get; set; }

        [FindsBy(How = How.CssSelector, Using = "body > div.sk-l-main-contaier > div.sk-l-content-wrapper > div.sk-l-content-full-width > div:nth-child(4) > div > div.sk-messages-conversations-right-pane.sk-messages-conversations-active-pane > div.nano.has-scrollbar > div.nano-content > div.sk-message-conversation-thread > div > div.sk-message-item-header")]
        private IWebElement ArchiveBodyFeedback { get; set; }

        [FindsBy(How = How.Id, Using = "sk-messageconversations-delete-dialog_inner")]
        private IWebElement DeletePopUp { get; set; }


        [FindsBy(How = How.CssSelector, Using = "#sk-messageconversations-delete-dialog-confirm-button")]
        private IWebElement ButtonDeletePopUp { get; set; }


        public Messages(IWebDriver driver, Users userrole)
        {
            _driver = driver;
            PageFactory.InitElements(_driver, this);

            switch (userrole)
            {
                case Users.Parent: case Users.Parent1: case Users.Parent2:
                    _driver.FindElement(By.XPath("/html/body/div[1]/div[1]/div/div[1]/nav/ul/li[2]/a")).Click();
                    break;

                case Users.Teacher1: case Users.Teacher:
                    // open Messages page
                    _driver.Navigate().GoToUrl($"{TestHost}/staff/messages/conversations");
                    break;

                case Users.Student:
                    _driver.Navigate().GoToUrl($"{TestHost}/student/messages/conversations");
                    break;
            }

            // wait for a create page loading
            new WebDriverWait(_driver, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.XPath("//input[@class='sk-searchsubmit-input']")));

        }

        public void SwitchTo(Tabs tab)
        {
            switch (tab)
            {
                case Tabs.Conversations:
                    Conversations.Click();
                    new WebDriverWait(_driver, TimeSpan.FromSeconds(10)).Until(ElementIsVisible(Conversations));
                    break;
                /*case Tabs.Inbox:
                    Inbox.Click();
                    new WebDriverWait(_driver, TimeSpan.FromSeconds(10)).Until(ElementIsVisible(Inbox));
                    break;*/
                case Tabs.Outbox:
                    Outbox.Click();
                    new WebDriverWait(_driver, TimeSpan.FromSeconds(10)).Until(ElementIsVisible(Outbox));
                    break;
                case Tabs.Draft:
                    Draft.Click();
                    new WebDriverWait(_driver, TimeSpan.FromSeconds(10)).Until(ElementIsVisible(Draft));
                    break;
                case Tabs.Unread:
                    Unread.Click();
                    new WebDriverWait(_driver, TimeSpan.FromSeconds(10)).Until(ElementIsVisible(Unread));
                    break;
                case Tabs.Archive:
                    Archive.Click();
                    new WebDriverWait(_driver, TimeSpan.FromSeconds(10)).Until(ElementIsVisible(Archive));
                    break;
            }
        }

        public void NewMessage(UserEntity to, string subject, string body, string cc = "", string bcc = "")
        {
            NewMessageButton.Click();

            // wait for a create message page loading
            new WebDriverWait(_driver, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.XPath("//div[@class='sk-new-message-title']")));

            AddCcBcc.Click();
            FieldTo.SendKeys(to.UserName);

            new WebDriverWait(_driver, TimeSpan.FromSeconds(10)).Until(ElementIsVisible(FieldToMultiselect));

            IWebElement recepientList =
                driver.FindElement(By.XPath("//span[contains(@title,'" + to.DisplayName + "')]"));
            recepientList.Click();
            FieldCc.SendKeys(cc);
            FieldBcc.SendKeys(bcc);
            FieldSubject.SendKeys(subject);

            // Switch to CKEditor iframe
            driver.SwitchTo().Frame(CKEditorFrame);
            IWebElement CKEditorBody = driver.FindElement(By.XPath("/html/body"));
            CKEditorBody.Click();
            CKEditorBody.Clear();
            CKEditorBody.SendKeys(body);

            // Back from iframe and save
            driver.SwitchTo().Window(driver.CurrentWindowHandle);
            ButtonSend.Click();

            //Check Feedback appear
            var feedback = new WebDriverWait(_driver, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.XPath(".//*[@id='genericFeedbackBaseLayout_text']")));
            Assert.IsTrue(feedback.GetAttribute("class").Contains(CommonCssClasses.Controls.Feedback.Text));
        }

        public void OpenMessage(UserEntity to, string subject, string body, string cc = "", string bcc = "")
        {
            SwitchTo(Tabs.Conversations);


        }

        public void TopMenuDelete(UserEntity to, string subject)
        {

            IWebElement messageleftmenu =
                driver.FindElement(By.XPath("//*[@class='sk-message-title']//*[text()='" + subject + "']"));  
            
            messageleftmenu.Click();

            new WebDriverWait(_driver, TimeSpan.FromSeconds(10)).Until(ElementIsVisible(ButtonDeleteTop));

            ButtonDeleteTop.Click();

            new WebDriverWait(_driver, TimeSpan.FromSeconds(10)).Until(ElementIsVisible(DeletePopUp));

            ButtonDeletePopUp.Click();

            //Check Feedback appear
            var feedback = new WebDriverWait(_driver, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.XPath(".//*[@id='genericFeedbackBaseLayout_text']")));
            Assert.IsTrue(feedback.GetAttribute("class").Contains(CommonCssClasses.Controls.Feedback.Text));


        } 
}
}
using System;
using Maksim.Web.SeleniumTests.Resources;
using Web.UI.Controls.Common.Utils;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;


namespace Maksim.Web.SeleniumTests.Pages
{
    class Documents : BaseTest 
    {
        private IWebDriver _driverGC;

        public Documents(IWebDriver driver, Users userrole)
        {
            _driverGC = driver;
            PageFactory.InitElements(_driverGC, this);

            switch (userrole)
            {
                case Users.Parent1:
                    // Go to  Contact book Page
                    _driverGC.FindElement(By.XPath("/html/body/div[1]/div[1]/div/div[1]/nav/ul/li[10]")).Click(); 
                    _driverGC.FindElement(By.Id("class")).Click();
                    break;
                case Users.Teacher1:
                    // Go to  Contact book Page
                    _driverGC.Navigate().GoToUrl($"{TestHost}/staff/documents/list");
                    break;
                case Users.Teacher:
                    // Go to  Contact book Page
                    _driverGC.Navigate().GoToUrl($"{TestHost}/staff/documents/list");
                    break;
                case Users.Student:
                    // Go to  Contact book Page
                    _driverGC.Navigate().GoToUrl($"{TestHost}/student/documents/list");
                    break;

            }


            //Wait for page loading
            _driverGC.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);

        }

        public void UploadDocumentByTeacher()
        {
            // Press on a button Upload new File
            _driverGC.FindElement(By.Id("addNewDocumentButtonClientId")).Click();

            // Wait for a pop up appear
            var uploadpopoup = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.Id("DocumentFileUploadDialog-css-modal_inner")));

            Assert.IsTrue(uploadpopoup.GetAttribute("Class").Contains("ccl-cssmodal-modal-inner"));

            //Click on a upload button
            var element = _driverGC.FindElement(By.Id("fileUploader_input"));

            //Upload file 
            element.SendKeys(ResourceManager.GenerateFullPath(ResourceManager.GetJpgFile()));

            // Wait for a file uploading
            var noprogressbar = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.InvisibilityOfElementLocated(By.ClassName("progress")));

            Assert.IsTrue(noprogressbar);

            // Press on a button ok
            _driverGC.FindElement(By.Id("DocumentFileUploadDialogOkOrCancelOkButtonId")).Click();

            // Wait for a file show on a page
            var test = "//*[@class=\"sk-documents-document-title\" and contains(text(),'SeleniumJpg.jpg')]";

            var uploadedfilepage = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.XPath(test)));
            
            Assert.AreEqual(true, uploadedfilepage.Displayed);
        }


        public void EditDocumentByTeacher()
        {

            //Wait for page loading
            _driverGC.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);

            //Open uploaded document for edit
            _driverGC.FindElement(By.XPath($"//span[text() = 'SeleniumJpg.jpg']/../../../..//*[@class=\"sk-context-menu-link\"]")).Click();

            // Wait for pop up loaded
            var popupmenuloaded = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.Id("sk-context-menu-container")));

            Assert.IsTrue(popupmenuloaded.GetAttribute("class").Contains("ccl-dropdownmenu"));

            //press edit
            _driverGC.FindElement(By.ClassName("sk-context-menu-blue-item")).Click();

            // Wait for pop up loaded
            var popuploaded = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.Id("DocumentsRenameFolderDialog-css-modal_header")));

            Assert.IsTrue(popuploaded.GetAttribute("class").Contains("ccl-cssmodal-modal-header"));

            _driverGC.FindElement(By.Id("sk-rename-document-dialog-input")).SendKeys(".Edited");

            _driverGC.FindElement(By.CssSelector("#EditDocumentPropertiesDialogInstanceCssModal_content > div > div.sk-dialog-checkbox > div > ins")).Click();

            _driverGC.FindElement(By.CssSelector("#EditDocumentPropertiesDialogInstanceCssModal_footer > div > input")).Click();

            // Check feedback about edit

            var feedback = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.CssSelector("#genericFeedbackBaseLayout_text")));

            Assert.IsTrue(feedback.GetAttribute("class").Contains(CommonCssClasses.Controls.Feedback.Text));

            // Wait for a file show on a page
            var lockicon = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.XPath("//span[text() = 'SeleniumJpg.Edited.jpg']/../../../..//*[@class=\"sk-documents-protected-document-indicator\"]")));

            Assert.IsTrue(lockicon.GetAttribute("class").Contains("sk-documents-protected-document-indicator"));


            // Wait for a file show on a page
            var uploadedfilepage = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.XPath("//*[@class=\"sk-documents-document-title\" and contains(text(),'SeleniumJpg.Edited.jpg')]")));

            Assert.AreEqual(true, uploadedfilepage.Displayed);
            
            // Check feedback dissappear
            var popupfeedback = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(20)).Until(
                ExpectedConditions.InvisibilityOfElementLocated(By.Id("genericFeedbackBaseLayout_container")));

            Assert.IsTrue(popupfeedback);

        }

        public void DeleteDocumentByTeacher()
        {
            //Wait for page loading
            _driverGC.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);

            //Open uploaded document for edit
            _driverGC.FindElement(By.XPath($"//span[text() = 'SeleniumJpg.jpg']/../../../..//*[@class=\"sk-context-menu-link\"]")).Click();


            // Wait for pop up loaded
            var popupmenuloaded = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.Id("sk-context-menu-container")));

            Assert.IsTrue(popupmenuloaded.GetAttribute("class").Contains("ccl-dropdownmenu h-no-print sk-context-menu-container"));

            //press delete
            _driverGC.FindElement(By.ClassName("sk-context-menu-red-item")).Click();

            // Wait for pop up loaded
            var popuploaded = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.Id("RemoveConfirmationContainerClientId_header")));

            Assert.IsTrue(popuploaded.GetAttribute("class").Contains("ccl-cssmodal-modal-header"));

            _driverGC.FindElement(By.CssSelector("#RemoveConfirmationContainerClientId_content > div > ul > li:nth-child(2) > div > ins")).Click();

            _driverGC.FindElement(By.Id("RemoveConfirmationOkCancelOkButtonId")).Click();

            // Check feedback about delete

            var feedback = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.CssSelector("#genericFeedbackBaseLayout_text")));

            Assert.IsTrue(feedback.GetAttribute("class").Contains(CommonCssClasses.Controls.Feedback.Text));

            // document deleted
            var docdelete = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//span[text() = 'SeleniumJpg.jpg']")));

            Assert.IsTrue(docdelete);

        }

        public void DeleteEditedDocumentByTeacher()
        {
            //Wait for page loading
            _driverGC.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);

            //Open 3dots menu
            _driverGC.FindElement(By.XPath($"//span[text() = 'SeleniumJpg.Edited.jpg']/../../../..//*[@class=\"sk-context-menu-link\"]")).Click();

            // Wait for pop up 3dots menu loaded
            var popupmenuloaded = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.Id("sk-context-menu-container")));

            Assert.IsTrue(popupmenuloaded.GetAttribute("class").Contains("ccl-dropdownmenu h-no-print sk-context-menu-container"));

            //press delete
            _driverGC.FindElement(By.XPath("//div[div[contains(@class, 'ccl-dropdownmenu-menuitem-selected')]]//li[contains(@class, 'sk-documents-context-menu-remove-link')]/a[1]")).Click();

            // Wait for delete pop up loaded
            var popuploaded = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.Id("RemoveConfirmationContainerClientId_header")));

            Assert.IsTrue(popuploaded.GetAttribute("class").Contains("ccl-cssmodal-modal-header"));

            _driverGC.FindElement(By.Id("RemoveConfirmationOkCancelOkButtonId")).Click();

            // Check feedback about delete

            var feedback = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.CssSelector("#genericFeedbackBaseLayout_text")));

            Assert.IsTrue(feedback.GetAttribute("class").Contains(CommonCssClasses.Controls.Feedback.Text));

            // document deleted
            var docdelete = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//span[text() = 'SeleniumJpg.Edited.jpg']")));

            Assert.IsTrue(docdelete);

        }

        public void EditDocumentNameWithErrorsByTeacher()
        {
            //Wait for page loading
            _driverGC.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);

            //Open uploaded document for edit
            _driverGC.FindElement(By.XPath($"//span[text() = 'SeleniumJpg.jpg']/../../../..//*[@class=\"sk-context-menu-link\"]")).Click();

            // Wait for pop up loaded
            var popupmenuloaded = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.Id("sk-context-menu-container")));

            Assert.IsTrue(popupmenuloaded.GetAttribute("class").Contains("ccl-dropdownmenu"));

            //press edit
            _driverGC.FindElement(By.ClassName("sk-context-menu-blue-item")).Click();

            // Wait for pop up loaded
            var popuploaded = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.Id("DocumentsRenameFolderDialog-css-modal_header")));

            Assert.IsTrue(popuploaded.GetAttribute("class").Contains("ccl-cssmodal-modal-header"));

            //Clear field
            _driverGC.FindElement(By.Id("sk-rename-document-dialog-input")).Clear();

            //Wrong symbols
            _driverGC.FindElement(By.Id("sk-rename-document-dialog-input")).SendKeys("@#$%^&*");

            _driverGC.FindElement(By.Id("sk-rename-document-dialog-input")).SendKeys(Keys.Enter);

            // Wait for error message
            var wrongsymbols = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.Id("sk-edit-document-validation-summary")));

            Assert.AreEqual(true, wrongsymbols.FindElement(By.Id("sk-edit-document-validation-summary")).Displayed); 

            //Empty name 
            _driverGC.FindElement(By.Id("sk-rename-document-dialog-input")).Clear();

            _driverGC.FindElement(By.Id("sk-rename-document-dialog-input")).SendKeys(Keys.Enter);

            // Wait for error message
            var emptyname = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.Id("sk-edit-document-validation-summary")));

            Assert.AreEqual(true, emptyname.FindElement(By.Id("sk-edit-document-validation-summary")).Displayed);

            //Cancel
            _driverGC.FindElement(By.Id("RenameDocumentsDialogOkCancelCancelLinkId")).Click();

            // Wait for a file show on a page
            var test = "//*[@class=\"sk-documents-document-title\" and contains(text(),'SeleniumJpg.jpg')]";

            var uploadedfilepage = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.XPath(test)));

            Assert.AreEqual(true, uploadedfilepage.Displayed);

        }


        public void UploadDocumentByParent()
        {
            // Press on a button Upload new File
            _driverGC.FindElement(By.Id("addNewDocumentButtonClientId")).Click();

            // Wait for a pop up appear
            var uploadpopoup = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.Id("DocumentFileUploadDialog-css-modal_inner")));

            Assert.IsTrue(uploadpopoup.GetAttribute("Class").Contains("ccl-cssmodal-modal-inner"));

            //Click on a upload button
            var element = _driverGC.FindElement(By.Id("fileUploader_input"));

            //Upload file 
            element.SendKeys(ResourceManager.GenerateFullPath(ResourceManager.GetJpgFile()));

            // Wait for a file uploading
            var noprogressbar = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.InvisibilityOfElementLocated(By.ClassName("progress")));

            Assert.IsTrue(noprogressbar);

            // Press on a button Ok
            _driverGC.FindElement(By.Id("DocumentFileUploadDialogOkOrCancelOkButtonId")).Click();

            //Wait for page loading
            _driverGC.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);

            // Wait for a file show on a page
            var test = "//*[@class=\"sk-documents-document-title\" and contains(text(),'SeleniumJpg.jpg')]";

            var uploadedfilepage = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.XPath(test)));

            Assert.AreEqual(true, uploadedfilepage.Displayed);

        }


        public void EditDocumentByParent()
        {

            //Wait for page loading
            _driverGC.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);

            //Open uploaded document for edit
            _driverGC.FindElement(By.XPath($"//span[text() = 'SeleniumJpg.jpg']/../../../..//*[@class=\"sk-context-menu-link\"]")).Click();

            // Wait for pop up loaded
            var popupmenuloaded = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.Id("sk-context-menu-container")));

            Assert.IsTrue(popupmenuloaded.GetAttribute("class").Contains("ccl-dropdownmenu"));

            //press edit
            _driverGC.FindElement(By.ClassName("sk-context-menu-blue-item")).Click();

            // Wait for pop up loaded
            var popuploaded = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.Id("DocumentsRenameFolderDialog-css-modal_header")));

            Assert.IsTrue(popuploaded.GetAttribute("class").Contains("ccl-cssmodal-modal-header"));

            _driverGC.FindElement(By.Id("sk-rename-document-dialog-input")).SendKeys(".Edited");

            _driverGC.FindElement(By.CssSelector("#EditDocumentPropertiesDialogInstanceCssModal_footer > div > input")).Click();

            // Check feedback about edit

            var feedback = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.CssSelector("#genericFeedbackBaseLayout_text")));

            Assert.IsTrue(feedback.GetAttribute("class").Contains(CommonCssClasses.Controls.Feedback.Text));

            // Wait for a file show on a page
            var uploadedfilepage = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.XPath($"//span[@class=\"sk-documents-document-title\" and contains(text(),'SeleniumJpg.Edited.jpg')]")));

            Assert.AreEqual(true, uploadedfilepage.Displayed);

            // Check feedback dissappear
            var popupfeedback = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(20)).Until(
                ExpectedConditions.InvisibilityOfElementLocated(By.Id("genericFeedbackBaseLayout_container")));

            Assert.IsTrue(popupfeedback);

        }

        public void DeleteDocumentByParent()
        {
            //Wait for page loading
            _driverGC.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);

            //Open uploaded document for edit
            _driverGC.FindElement(By.XPath($"//span[text() = 'SeleniumJpg.jpg']/../../../..//*[@class=\"sk-context-menu-link\"]")).Click();

            // Wait for pop up loaded
            var popupmenuloaded = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.Id("sk-context-menu-container")));

            Assert.IsTrue(popupmenuloaded.GetAttribute("class").Contains("ccl-dropdownmenu"));

            //press edit
            _driverGC.FindElement(By.ClassName("sk-context-menu-red-item")).Click();

            // Wait for pop up loaded
            var popuploaded = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.Id("RemoveConfirmationContainerClientId_header")));

            Assert.IsTrue(popuploaded.GetAttribute("class").Contains("ccl-cssmodal-modal-header"));

            _driverGC.FindElement(By.Id("RemoveConfirmationOkCancelOkButtonId")).Click();

            // Check feedback about delete

            var feedback = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.CssSelector("#genericFeedbackBaseLayout_text")));

            Assert.IsTrue(feedback.GetAttribute("class").Contains(CommonCssClasses.Controls.Feedback.Text));

            // document deleted
            var docdelete = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//span[text() = 'SeleniumJpg.jpg']")));

            Assert.IsTrue(docdelete);

        }

        public void DeleteEditedDocumentByParent()
        {
            //Wait for page loading
            _driverGC.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);

            //Open uploaded document for edit
            _driverGC.FindElement(By.XPath($"//span[text() = 'SeleniumJpg.Edited.jpg']/../../../..//*[@class=\"sk-context-menu-link\"]")).Click();

            // Wait for pop up loaded
            var popupmenuloaded = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.Id("sk-context-menu-container")));

            Assert.IsTrue(popupmenuloaded.GetAttribute("class").Contains("ccl-dropdownmenu"));

            //press edit
            _driverGC.FindElement(By.ClassName("sk-context-menu-red-item")).Click();

            // Wait for pop up loaded
            var popuploaded = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.Id("RemoveConfirmationContainerClientId_header")));

            Assert.IsTrue(popuploaded.GetAttribute("class").Contains("ccl-cssmodal-modal-header"));

            _driverGC.FindElement(By.Id("RemoveConfirmationOkCancelOkButtonId")).Click();

            // Check feedback about delete

            var feedback = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.CssSelector("#genericFeedbackBaseLayout_text")));

            Assert.IsTrue(feedback.GetAttribute("class").Contains(CommonCssClasses.Controls.Feedback.Text));

            // document deleted
            var docdelete = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//span[text() = 'SeleniumJpg.Edited.jpg']")));

            Assert.IsTrue(docdelete);

        }

        public void UploadDocumentByStudent()
        {
            // Press on a button Upload new File
            _driverGC.FindElement(By.Id("addNewDocumentButtonClientId")).Click();

            // Wait for a pop up appear
            var uploadpopoup = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.Id("DocumentFileUploadDialog-css-modal_inner")));

            Assert.IsTrue(uploadpopoup.GetAttribute("Class").Contains("ccl-cssmodal-modal-inner"));

            //Click on a upload button
            var element = _driverGC.FindElement(By.Id("fileUploader_input"));

            //Upload file 
            element.SendKeys(ResourceManager.GenerateFullPath(ResourceManager.GetJpgFile()));

            // Wait for a file uploading
            var noprogressbar = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.InvisibilityOfElementLocated(By.ClassName("progress")));

            Assert.IsTrue(noprogressbar);

            // Press on a button Ok
            _driverGC.FindElement(By.Id("DocumentFileUploadDialogOkOrCancelOkButtonId")).Click();

            //Wait for page loading
            _driverGC.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);

            // Wait for a file show on a page
            var test = "//*[@class=\"sk-documents-document-title\" and contains(text(),'SeleniumJpg.jpg')]";

            var uploadedfilepage = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.XPath(test)));

            Assert.AreEqual(true, uploadedfilepage.Displayed);
        }


        public void EditDocumentByStudent()
        {

            //Wait for page loading
            _driverGC.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);

            //Open uploaded document for edit
            _driverGC.FindElement(By.XPath($"//span[text() = 'SeleniumJpg.jpg']/../../../..//*[@class=\"sk-context-menu-link\"]")).Click();

            // Wait for pop up loaded
            var popupmenuloaded = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.Id("sk-context-menu-container")));

            Assert.IsTrue(popupmenuloaded.GetAttribute("class").Contains("ccl-dropdownmenu"));

            //press edit
            _driverGC.FindElement(By.ClassName("sk-context-menu-blue-item")).Click();

            // Wait for pop up loaded
            var popuploaded = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.Id("DocumentsRenameFolderDialog-css-modal_header")));

            Assert.IsTrue(popuploaded.GetAttribute("class").Contains("ccl-cssmodal-modal-header"));

            _driverGC.FindElement(By.Id("sk-rename-document-dialog-input")).SendKeys(".Edited");

            _driverGC.FindElement(By.CssSelector("#EditDocumentPropertiesDialogInstanceCssModal_footer > div > input")).Click();

            // Check feedback about edit

            var feedback = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.CssSelector("#genericFeedbackBaseLayout_text")));

            Assert.IsTrue(feedback.GetAttribute("class").Contains(CommonCssClasses.Controls.Feedback.Text));


            // Wait for a file show on a page
            var uploadedfilepage = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.XPath($"//span[@class=\"sk-documents-document-title\" and contains(text(),'SeleniumJpg.Edited.jpg')]")));

            Assert.AreEqual(true, uploadedfilepage.Displayed);

            // Check feedback dissappear
            var popupfeedback = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(20)).Until(
                ExpectedConditions.InvisibilityOfElementLocated(By.Id("genericFeedbackBaseLayout_container")));

            Assert.IsTrue(popupfeedback);


        }

        public void DeleteDocumentByStudent()
        {
            //Wait for page loading
            _driverGC.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);

            //Open uploaded document for edit
            _driverGC.FindElement(By.XPath($"//span[text() = 'SeleniumJpg.jpg']/../../../..//*[@class=\"sk-context-menu-link\"]")).Click();

            // Wait for pop up loaded
            var popupmenuloaded = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.Id("sk-context-menu-container")));

            Assert.IsTrue(popupmenuloaded.GetAttribute("class").Contains("ccl-dropdownmenu"));

            //press edit
            _driverGC.FindElement(By.ClassName("sk-context-menu-red-item")).Click();

            // Wait for pop up loaded
            var popuploaded = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.Id("RemoveConfirmationContainerClientId_header")));

            Assert.IsTrue(popuploaded.GetAttribute("class").Contains("ccl-cssmodal-modal-header"));

            _driverGC.FindElement(By.CssSelector("#RemoveConfirmationContainerClientId_content > div > ul > li:nth-child(2) > div > ins")).Click();

            _driverGC.FindElement(By.Id("RemoveConfirmationOkCancelOkButtonId")).Click();

            // Check feedback about delete

            var feedback = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.CssSelector("#genericFeedbackBaseLayout_text")));

            Assert.IsTrue(feedback.GetAttribute("class").Contains(CommonCssClasses.Controls.Feedback.Text));

            // document deleted
            var docdelete = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//span[text() = 'SeleniumJpg.jpg']")));

            Assert.IsTrue(docdelete);

        }

        public void DeleteEditedDocumentByStudent()
        {
            //Wait for page loading
            _driverGC.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);

            //Open uploaded document for edit
            _driverGC.FindElement(By.XPath($"//span[text() = 'SeleniumJpg.Edited.jpg']/../../../..//*[@class=\"sk-context-menu-link\"]")).Click();

            // Wait for pop up loaded
            var popupmenuloaded = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.Id("sk-context-menu-container")));

            Assert.IsTrue(popupmenuloaded.GetAttribute("class").Contains("ccl-dropdownmenu"));

            //press edit
            _driverGC.FindElement(By.ClassName("sk-context-menu-red-item")).Click();

            // Wait for pop up loaded
            var popuploaded = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.Id("RemoveConfirmationContainerClientId_header")));

            Assert.IsTrue(popuploaded.GetAttribute("class").Contains("ccl-cssmodal-modal-header"));

            _driverGC.FindElement(By.CssSelector("#RemoveConfirmationContainerClientId_content > div > ul > li:nth-child(2) > div > ins")).Click();

            _driverGC.FindElement(By.Id("RemoveConfirmationOkCancelOkButtonId")).Click();

            // Check feedback about delete

            var feedback = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.ElementExists(By.CssSelector("#genericFeedbackBaseLayout_text")));

            Assert.IsTrue(feedback.GetAttribute("class").Contains(CommonCssClasses.Controls.Feedback.Text));

            // document deleted
            var docdelete = new WebDriverWait(_driverGC, TimeSpan.FromSeconds(10)).Until(
                ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//span[text() = 'SeleniumJpg.Edited.jpg']")));

            Assert.IsTrue(docdelete);

        }

    }
}

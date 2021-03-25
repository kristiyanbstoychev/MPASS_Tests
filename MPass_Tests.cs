using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading;

namespace MPass_Tests
{
    public class MPass_Tests
    {
        IWebDriver driver;

        [OneTimeSetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
        }

        [Test, Order(1)]
        public void Succesfull_Registration()
        {
            driver.Navigate().GoToUrl("https://sofia.mpass.bg/bg/");
            var LinkRegistration = driver.FindElement(By.XPath("//a[@href='/bg/register']"));
            LinkRegistration.Click();

            var inputFirstName = driver.FindElement(By.Id("first_name"));
            var inputLastName = driver.FindElement(By.Id("last_name"));
            var inputEmail = driver.FindElement(By.Id("email"));
            var inputPassword = driver.FindElement(By.Id("password"));

            var checkBoxTos = driver.FindElement(By.Id("has_agreed_to_tos"));
            var checkBoxPolicy = driver.FindElement(By.Id("has_agreed_to_privacy_policy"));

            var buttonRegister = driver.FindElement(By.XPath("(//span[contains(.,'Регистрация')])[3]"));

            Random rnd = new Random();
            int randomNum = rnd.Next(0, 1000);

            inputFirstName.SendKeys("firstName" + randomNum);
            inputLastName.SendKeys("lastName" + randomNum);
            inputEmail.SendKeys("testUser" + randomNum + "@test.com");
            inputPassword.SendKeys("password" + randomNum + randomNum);

            checkBoxTos.Click();
            checkBoxPolicy.Click();

            buttonRegister.Click();
            Thread.Sleep(3000);
            var successMessage = driver.FindElement(By.XPath("/html/body/app-root/div[2]/div/div/app-register/div/div[2]/h4"));
            var buttonContinue = driver.FindElement(By.XPath("//a[contains(@class,'btn btn-primary my-3')]"));
            var navBarMenu = driver.FindElement(By.XPath("/html/body/app-root/div[2]/div/app-header/div/nav/div/div[1]/button"));

            Assert.IsTrue(successMessage.Displayed);
            Assert.IsTrue(buttonContinue.Displayed);
            Assert.IsTrue(navBarMenu.Displayed);

            navBarMenu.Click();

            var logOutButton = driver.FindElement(By.XPath("/html/body/app-root/div[2]/div/app-header/div/nav/div/div[2]/ul/li[8]/a/span"));
            logOutButton.Click();

            Thread.Sleep(1000);

        }

        [Test, Order(2)]
        public void Registration_with_invalid_Email()
        {
            driver.Navigate().GoToUrl("https://sofia.mpass.bg/bg/");
            var LinkRegistration = driver.FindElement(By.XPath("//a[@href='/bg/register']"));
            LinkRegistration.Click();

            var inputFirstName = driver.FindElement(By.Id("first_name"));
            var inputLastName = driver.FindElement(By.Id("last_name"));
            var inputEmail = driver.FindElement(By.Id("email"));
            var inputPassword = driver.FindElement(By.Id("password"));

            var checkBoxTos = driver.FindElement(By.Id("has_agreed_to_tos"));
            var checkBoxPolicy = driver.FindElement(By.Id("has_agreed_to_privacy_policy"));

            var buttonRegister = driver.FindElement(By.XPath("(//span[contains(.,'Регистрация')])[3]"));

            Random rnd = new Random();
            int randomNum = rnd.Next(0, 1000);

            inputFirstName.SendKeys("firstName" + randomNum);
            inputLastName.SendKeys("lastName" + randomNum);
            inputEmail.SendKeys("testUser" + randomNum);
            inputPassword.SendKeys("password" + randomNum + randomNum);

            checkBoxTos.Click();
            checkBoxPolicy.Click();

            buttonRegister.Click();
            Thread.Sleep(1000);
            var errorMsg = driver.FindElement(By.XPath("(//div[contains(@class,'invalid-feedback')])[3]"));

            Assert.IsTrue(errorMsg.Displayed);
            Assert.AreEqual("Въведете валиден имейл адрес.", errorMsg.Text);
        }

        [Test, Order(3)]
        public void Registration_with_invalid_Password()
        {
            driver.Navigate().GoToUrl("https://sofia.mpass.bg/bg/");
            var LinkRegistration = driver.FindElement(By.XPath("//a[@href='/bg/register']"));
            LinkRegistration.Click();

            var inputFirstName = driver.FindElement(By.Id("first_name"));
            var inputLastName = driver.FindElement(By.Id("last_name"));
            var inputEmail = driver.FindElement(By.Id("email"));
            var inputPassword = driver.FindElement(By.Id("password"));

            var checkBoxTos = driver.FindElement(By.Id("has_agreed_to_tos"));
            var checkBoxPolicy = driver.FindElement(By.Id("has_agreed_to_privacy_policy"));

            var buttonRegister = driver.FindElement(By.XPath("(//span[contains(.,'Регистрация')])[3]"));

            Random rnd = new Random();
            int randomNum = rnd.Next(0, 1000);

            inputFirstName.SendKeys("firstName" + randomNum);
            inputLastName.SendKeys("lastName" + randomNum);
            inputEmail.SendKeys("testUser" + randomNum + "test.com");
            inputPassword.SendKeys("131313123123");

            checkBoxTos.Click();
            checkBoxPolicy.Click();

            buttonRegister.Click();
            Thread.Sleep(1000);
            var errorMsg = driver.FindElement(By.XPath("(//div[contains(@class,'invalid-feedback')])[4]"));

            Assert.IsTrue(errorMsg.Displayed);
            Assert.AreEqual("Тази парола е изцяло от цифри.", errorMsg.Text);
        }

        [Test, Order(4)]
        public void Login()
        {
            driver.Navigate().GoToUrl("https://sofia.mpass.bg/bg/");
            var buttonLogin = driver.FindElement(By.XPath("//a[@href='/bg/login']"));
            buttonLogin.Click();

            var inputEmail = driver.FindElement(By.Id("email"));
            var inputPassword = driver.FindElement(By.Id("password"));

            inputEmail.SendKeys("testuser@testuser.com");
            inputPassword.SendKeys("testpassword12345");
            
            var buttonContinue = driver.FindElement(By.ClassName("loader"));
            buttonContinue.Click();
            Thread.Sleep(3000);
            var linkPurchaseCard = driver.FindElement(By.XPath("(//a[contains(@routerlink,'/purchase')])[2]"));
            var navBarMenu = driver.FindElement(By.XPath("/html/body/app-root/div[2]/div/app-header/div/nav/div/div[1]/button"));

            Assert.IsTrue(linkPurchaseCard.Displayed);
            Assert.IsTrue(navBarMenu.Displayed);
        }

        [Test, Order(5)]
        public void Change_password()
        {
            driver.Navigate().GoToUrl("https://sofia.mpass.bg/bg/");
            Thread.Sleep(1000);
            var navBarMenu = driver.FindElement(By.XPath("/html/body/app-root/div[2]/div/app-header/div/nav/div/div[1]/button"));
            navBarMenu.Click();
                
            var profileMenu = driver.FindElement(By.XPath("//span[contains(.,'Профил')]"));
            profileMenu.Click();

            Thread.Sleep(1000);
            var buttonChangePassword = driver.FindElement(By.XPath("/html/body/app-root/div[2]/div/div/app-profile/div/div/div/form/div[5]/div/div/button"));
            buttonChangePassword.Click();

            Thread.Sleep(1000);
            var oldPassword = driver.FindElement(By.Id("old_password"));
            var newPassword = driver.FindElement(By.Id("new_password"));

            oldPassword.SendKeys("testpassword12345");
            newPassword.SendKeys("newtestpassword12345");

            var buttonSavePassword = driver.FindElement(By.XPath("/html/body/app-root/div[2]/div/div/app-profile/div/div/div/app-password-change/form/fieldset/div[3]/div/button[2]/span/span[1]/span"));
            buttonSavePassword.Click();

            Thread.Sleep(2000);

            var inputEmail = driver.FindElement(By.Id("email"));
            var inputPassword = driver.FindElement(By.Id("password"));
            var buttonContinue = driver.FindElement(By.XPath("/html/body/app-root/div[2]/div/div/app-login/div/div/div/form/div[3]/button"));

            inputEmail.SendKeys("testuser@testuser.com");
            inputPassword.SendKeys("newtestpassword12345");
            buttonContinue.Click();

            Thread.Sleep(1000);
            navBarMenu = driver.FindElement(By.XPath("/html/body/app-root/div[2]/div/app-header/div/nav/div/div[1]/button"));
            var linkPurchaseCard = driver.FindElement(By.XPath("(//a[contains(@routerlink,'/purchase')])[2]"));

            Assert.IsTrue(navBarMenu.Displayed);
            Assert.IsTrue(linkPurchaseCard.Displayed);
        }

        [Test, Order(6)]
        public void Change_First_and_Last_Name()
        {
            driver.Navigate().GoToUrl("https://sofia.mpass.bg/bg/");
            Thread.Sleep(1000);
            var navBarMenu = driver.FindElement(By.XPath("/html/body/app-root/div[2]/div/app-header/div/nav/div/div[1]/button"));
            navBarMenu.Click();

            var profileMenu = driver.FindElement(By.XPath("//span[contains(.,'Профил')]"));
            profileMenu.Click();
            Thread.Sleep(1000);

            var inputFirstName = driver.FindElement(By.Id("first_name"));
            var inputLastName = driver.FindElement(By.Id("last_name"));
            var buttonSave = driver.FindElement(By.XPath("/html/body/app-root/div[2]/div/div/app-profile/div/div/div/form/div[6]/button"));

            var changedFirstName = "newFirstName";
            var changedLastName = "newLastName";
            inputFirstName.Clear();
            inputLastName.Clear();
            inputFirstName.SendKeys(changedFirstName);
            inputLastName.SendKeys(changedLastName);
            buttonSave.Click();
            Thread.Sleep(1000);

            navBarMenu = driver.FindElement(By.XPath("/html/body/app-root/div[2]/div/app-header/div/nav/div/div[1]/button"));
            navBarMenu.Click();

            profileMenu = driver.FindElement(By.XPath("//span[contains(.,'Профил')]"));
            profileMenu.Click();
            Thread.Sleep(3000);

            inputFirstName = driver.FindElement(By.Id("first_name"));
            inputLastName = driver.FindElement(By.Id("last_name"));

            var inputFirstNameText = inputFirstName.GetAttribute("value");
            var inputLastNameText = inputLastName.GetAttribute("value");

            Assert.AreEqual(changedFirstName, inputFirstNameText);
            Assert.AreEqual(changedLastName, inputLastNameText);
        }

        [Test, Order(7)]
        public void Purchase_Page_Content()
        {
            driver.Navigate().GoToUrl("https://sofia.mpass.bg/bg/");
            Thread.Sleep(1000);
            var linkPurchaseCard = driver.FindElement(By.XPath("(//a[contains(@routerlink,'/purchase')])[2]"));

            linkPurchaseCard.Click();
            Thread.Sleep(2000);

            var dailyCardOption = driver.FindElement(By.XPath("/html/body/app-root/div[2]/div/div/app-tariffs-list/div/div[1]"));
            var oneTimeMetroCard = driver.FindElement(By.XPath("/html/body/app-root/div[2]/div/div/app-tariffs-list/div/div[2]"));
            var oneTimeBusCard = driver.FindElement(By.XPath("/html/body/app-root/div[2]/div/div/app-tariffs-list/div/div[3]"));

            Assert.IsTrue(dailyCardOption.Displayed);
            Assert.IsTrue(oneTimeMetroCard.Displayed);
            Assert.IsTrue(oneTimeBusCard.Displayed);

        }

        [Test, Order(8)]
        public void Daily_Card_Purchase_Page()
        {
            driver.Navigate().GoToUrl("https://sofia.mpass.bg/bg/");
            Thread.Sleep(1000);
            var linkPurchaseCard = driver.FindElement(By.XPath("(//a[contains(@routerlink,'/purchase')])[2]"));
           
            linkPurchaseCard.Click();
            Thread.Sleep(2000);

            var buttonDailyCardOption = driver.FindElement(By.XPath("/html/body/app-root/div[2]/div/div/app-tariffs-list/div/div[1]/div[3]/button[2]"));
            
            buttonDailyCardOption.Click();
            Thread.Sleep(2000);
            var paymentMethodDropDown = driver.FindElement(By.XPath("//div[contains(@class,'payment-instrument__content ng-star-inserted')]"));
            var termsOfUse = driver.FindElement(By.XPath("/html/body/app-root/div[2]/div/div/app-payment-method/div/div/div[1]/div[2]/a"));
            var paymentButton = driver.FindElement(By.XPath("/html/body/app-root/div[2]/div/div/app-payment-method/div/div/div[2]/fieldset/div/button[2]"));
            var backButton = driver.FindElement(By.XPath("/html/body/app-root/div[2]/div/div/app-payment-method/div/div/div[2]/fieldset/div/button[1]"));

            Assert.IsTrue(paymentMethodDropDown.Displayed);
            Assert.IsTrue(termsOfUse.Displayed);
            Assert.IsTrue(paymentButton.Displayed);
            Assert.IsTrue(backButton.Displayed);

        }

        [Test, Order(9)]
        public void One_Time_Metro_Card_Purchase_Page()
        {
            driver.Navigate().GoToUrl("https://sofia.mpass.bg/bg/");
            Thread.Sleep(1000);
            var linkPurchaseCard = driver.FindElement(By.XPath("(//a[contains(@routerlink,'/purchase')])[2]"));
            
            linkPurchaseCard.Click();
            Thread.Sleep(2000);

            var buttonOneTimeMetroCard = driver.FindElement(By.XPath("/html/body/app-root/div[2]/div/div/app-tariffs-list/div/div[2]/div[3]/button[2]"));

            buttonOneTimeMetroCard.Click();
            Thread.Sleep(2000);
            var paymentMethodDropDown = driver.FindElement(By.XPath("//div[contains(@class,'payment-instrument__content ng-star-inserted')]"));
            var termsOfUse = driver.FindElement(By.XPath("/html/body/app-root/div[2]/div/div/app-payment-method/div/div/div[1]/div[2]/a"));
            var paymentButton = driver.FindElement(By.XPath("/html/body/app-root/div[2]/div/div/app-payment-method/div/div/div[2]/fieldset/div/button[2]"));
            var backButton = driver.FindElement(By.XPath("/html/body/app-root/div[2]/div/div/app-payment-method/div/div/div[2]/fieldset/div/button[1]"));

            Assert.IsTrue(paymentMethodDropDown.Displayed);
            Assert.IsTrue(termsOfUse.Displayed);
            Assert.IsTrue(paymentButton.Displayed);
            Assert.IsTrue(backButton.Displayed);

        }

        [Test, Order(10)]
        public void One_Time_Bus_Card_Purchase_Page()
        {
            driver.Navigate().GoToUrl("https://sofia.mpass.bg/bg/");
            Thread.Sleep(1000);
            var linkPurchaseCard = driver.FindElement(By.XPath("(//a[contains(@routerlink,'/purchase')])[2]"));
            
            linkPurchaseCard.Click();
            Thread.Sleep(2000);

            var buttonOneTimeBusCard = driver.FindElement(By.XPath("/html/body/app-root/div[2]/div/div/app-tariffs-list/div/div[3]/div[3]/button[2]"));

            buttonOneTimeBusCard.Click();
            Thread.Sleep(2000);
            var paymentMethodDropDown = driver.FindElement(By.XPath("//div[contains(@class,'payment-instrument__content ng-star-inserted')]"));
            var termsOfUse = driver.FindElement(By.XPath("/html/body/app-root/div[2]/div/div/app-payment-method/div/div/div[1]/div[2]/a"));
            var paymentButton = driver.FindElement(By.XPath("/html/body/app-root/div[2]/div/div/app-payment-method/div/div/div[2]/fieldset/div/button[2]"));
            var backButton = driver.FindElement(By.XPath("/html/body/app-root/div[2]/div/div/app-payment-method/div/div/div[2]/fieldset/div/button[1]"));

            Assert.IsTrue(paymentMethodDropDown.Displayed);
            Assert.IsTrue(termsOfUse.Displayed);
            Assert.IsTrue(paymentButton.Displayed);
            Assert.IsTrue(backButton.Displayed);

           
        }

        [Test, Order(11)]
        public void Logout()
        {
            driver.Navigate().GoToUrl("https://sofia.mpass.bg/bg/");
            Thread.Sleep(1000);
            var navBarMenu = driver.FindElement(By.XPath("/html/body/app-root/div[2]/div/app-header/div/nav/div/div[1]/button"));

            navBarMenu.Click();

            var logOutButton = driver.FindElement(By.XPath("/html/body/app-root/div[2]/div/app-header/div/nav/div/div[2]/ul/li[8]/a/span"));
            logOutButton.Click();

            Thread.Sleep(1000);

            var logInButton = driver.FindElement(By.XPath("//a[@href='/bg/login']"));
            var registerInButton = driver.FindElement(By.XPath("//a[@href='/bg/register']"));

            Assert.IsTrue(logInButton.Displayed);
            Assert.IsTrue(registerInButton.Displayed);
        }

        [OneTimeTearDown]
        public void ShutDown()
        {
            driver.Quit();
        }
    }
}
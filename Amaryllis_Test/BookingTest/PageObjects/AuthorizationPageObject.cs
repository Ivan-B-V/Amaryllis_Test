using BookingTest.Helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;

namespace BookingTest.PageObjects
{

    public class AuthorizationPageObject
    {
        //page URL: https://account.booking.com/
        private IWebDriver driver;

        By emailInput = By.XPath("//input[@type='email']");
        By passwordInput = By.XPath("//input[@type='password']"); // /span[text()='Continue with email']
        By continueButton = By.XPath("//button[@type='submit']");
        By signInButton = By.XPath("//button[@type='submit']");

        public AuthorizationPageObject(IWebDriver driver)
        {
            this.driver = driver;
        }

        public MainMenuPageObject Login(string login, string password)
        {   
            driver.FindElement(emailInput).SendKeys(login);
            SubmitButtonClick();

            Utils.WaitForElement(() => driver.FindElement(passwordInput).Displayed == true, TimeSpan.FromSeconds(5));

            driver.FindElement(passwordInput).SendKeys(password);
            SubmitButtonClick();
            return new MainMenuPageObject(driver);
        }

        public MainMenuPageObject SubmitButtonClick()
        {
            driver.FindElement(continueButton).Click();
            return new MainMenuPageObject(driver);
        }
    }
}

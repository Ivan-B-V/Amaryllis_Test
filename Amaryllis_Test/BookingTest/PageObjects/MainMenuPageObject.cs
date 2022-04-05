using BookingTest.Helpers;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace BookingTest.PageObjects
{
    public class MainMenuPageObject
    {
        private IWebDriver driver;

        private readonly By cngLangButton = By.XPath("//button[@data-tooltip-text='Choose your language']"); ////div[2]/div[2]/button
        private readonly By currentLangTitle = By.XPath("//button[@data-tooltip-text='Choose your language']/span/span");
        private readonly By bookinggoButton = By.XPath("//a[@data-decider-header='bookinggo']"); //bookinggo 
        private readonly By changeCurrencyButton = By.XPath("//button[@data-tooltip-text='Choose your currency']");
        private readonly By searchButton = By.XPath("//button[@class='sb-searchbox__button ']");
        private readonly By searchInput = By.XPath("//div[@data-component='search/destination/input']//input[@type='search']");
        private readonly By signInButton = By.XPath("//span[contains(text(), 'Sign in')]");
        private readonly By yourAccountBtn = By.XPath("//button[@aria-label='Your account']");
        private readonly By manageAccountBtn = By.XPath("//div[@role='menu']//span[text()='Manage account']/../../.."); //  ancestor::button


        public MainMenuPageObject(IWebDriver driver)
        {
            this.driver = driver;
        }

        public LanguagesPageObject ChooseLanguage()
        {
            driver.FindElement(cngLangButton).Click();

            return new LanguagesPageObject(driver);
        }

        public string GetLanguageButtonTitle()
        {
            return driver.FindElement(currentLangTitle).Text;
        }

        public MainMenuPageObject Search(string place)
        {
            driver.FindElement(searchInput).SendKeys(place);
            return this;
        }

        public SearchingPageObject SubmitSearch()
        {
            driver.FindElement(searchButton).Click();
            return new SearchingPageObject(driver);
        }

        public void GoToBookinggo()
        {
            driver.FindElement(bookinggoButton).Click();
        }

        public AuthorizationPageObject SignIn()
        {
            driver.FindElement(signInButton).Click();
            return new AuthorizationPageObject(driver);
        }

        public PersonalAccountPageObject GoToAccountSettings()
        {
            Utils.WaitForElement(() => driver.FindElement(yourAccountBtn).Displayed == true, TimeSpan.FromSeconds(5));
            driver.FindElement(yourAccountBtn).Click();
            Utils.WaitForElement(() => driver.FindElement(manageAccountBtn).Displayed == true, TimeSpan.FromSeconds(5));
            driver.FindElement(manageAccountBtn).Click();
            return new PersonalAccountPageObject(driver);
        }
    }
}

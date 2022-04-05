using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingTest.PageObjects
{
    public class LanguagesPageObject
    {
        private IWebDriver driver;

        public LanguagesPageObject(IWebDriver driver)
        {
            this.driver = driver;
        }

        public MainMenuPageObject SetLanguage(string lang)
        {
            By langButton = By.XPath($"//a[@data-lang='{lang}']");
            driver.FindElement(langButton).Click();
            return new MainMenuPageObject(driver);
        }
    }
}

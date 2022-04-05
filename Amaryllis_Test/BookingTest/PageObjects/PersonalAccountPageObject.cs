using OpenQA.Selenium;
using System.Threading;

namespace BookingTest.PageObjects
{
    public class PersonalAccountPageObject
    {
        IWebDriver driver;

        By personalDetails = By.XPath("//a[@aria-labelledby='mysettings_personal_details_title']");

        public PersonalAccountPageObject(IWebDriver driver)
        {
            this.driver = driver;
        }

        public PersonalDetailsPageObject GoToPersonalDetails()
        {
            Thread.Sleep(5000);
            driver.FindElement(personalDetails).Click();
            return new PersonalDetailsPageObject(driver);
        }
    }
}

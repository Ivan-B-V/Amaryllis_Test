using OpenQA.Selenium;
using System.Threading;

namespace BookingTest.PageObjects
{
    public class PersonalDetailsPageObject
    {
        IWebDriver driver;

        By emailArea = By.XPath("//div[@data-test-id='mysettings-row-email']//h2[text()='Email address']/../following-sibling::div/div/div/div");

        public PersonalDetailsPageObject(IWebDriver driver)
        {
            this.driver = driver;
        }

        public string GetEmail()
        {
            Thread.Sleep(5000);
            return driver.FindElement(emailArea).Text;
        }
    }
}

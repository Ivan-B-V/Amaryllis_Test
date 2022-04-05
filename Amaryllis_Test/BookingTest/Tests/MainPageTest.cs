using BookingTest.PageObjects;
using NUnit.Framework;
using System;

namespace BookingTest.Tests
{
    [TestFixture]
    public class MainPageTest : BaseTest
    {
        private readonly string language = "en-gb";
        private readonly string expectedLang = "English (UK)";

        [Test]

        public void ChangeLanguage()
        {
            var mainPage = new MainMenuPageObject(driver);

            var title = mainPage.ChooseLanguage().SetLanguage(language).GetLanguageButtonTitle();
            Assert.IsTrue(title.Contains(expectedLang));
        }

        [Test]
        public void GoToBookingGoPage()
        {
            var mainPage = new MainMenuPageObject(driver);
            mainPage.GoToBookinggo();
            Console.WriteLine(driver.Url);
            Assert.IsTrue(driver.Url.StartsWith(TestSettings.BookingCarRental));
        }
    }
}

using BookingTest.PageObjects;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Threading;

namespace BookingTest.Tests
{
    [TestFixture]
    public class AuthorizationPageTests : BaseTest
    {
        string email = "hammer000destroyer@gmail.com";
        string password = "GTHqqweasd123";

        [Test]
        public void LoginAsUserWithEmailAndPassword()
        {
            var mainPage = new MainMenuPageObject(driver);
            string actualEmail = mainPage.SignIn()
                    .Login(email, password)
                    .GoToAccountSettings()
                    .GoToPersonalDetails().GetEmail();

            Assert.AreEqual(email, actualEmail);
        }
    }
}
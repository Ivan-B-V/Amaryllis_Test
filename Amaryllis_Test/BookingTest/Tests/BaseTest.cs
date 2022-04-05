using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Support;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingTest.Tests
{
    public class BaseTest
    {
        protected IWebDriver driver;

        [OneTimeSetUp]
        protected void DoBeforeAllTests()
        {
            driver = new ChromeDriver();
        }

        [TearDown]
        protected void DoAfterEachTest()
        {
            driver.Close();
        }

        [SetUp]
        protected void DoBeforeEach()
        {
            driver.Manage().Cookies.DeleteAllCookies();
            driver.Navigate().GoToUrl(TestSettings.HostPrefix);
            driver.Manage().Window.Maximize();
        }
    }
}

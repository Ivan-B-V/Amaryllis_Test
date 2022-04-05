using BookingTest.Helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace BookingTest.PageObjects
{
    public class SearchingPageObject
    {

        private IWebDriver driver;

        //Calendar: //div[@class='bui-calendar'][@style='display: block;']
        //Next button: //div[@class='bui-calendar'][@style='display: block;']//div[@class='bui-calendar__control bui-calendar__control--next']
        //Date: //div[@class='bui-calendar'][@style='display: block;']//div[@class='bui-calendar__content']/div[1]//span[text()='1']  ?(/../..)
        By calendar = By.XPath("//div[@class='bui-calendar'][@style='display: block;']");
        By searchingRes = By.XPath("//div[@data-capla-component='b-search-web-searchresults/HeaderDesktop']//h1");
        By checkInDateBtn = By.XPath("//div[@data-mode='checkin']");
        By checkOutDateBtn = By.XPath("//div[@data-mode='checkout']");
        By people = By.XPath("//div[@data-component='search/group/group-with-modal']");
        By increaseAdults = By.XPath("//button[@aria-label='Increase number of Adults']");
        By increaseChildren = By.XPath("//button[@aria-label='Increase number of Children']");
        By increaseRooms = By.XPath("//button[@aria-label='Increase number of Rooms']");
        By doneBtn = By.XPath("//button[@id='doneBtn']");
        By searchButton = By.XPath("//button[@class='sb-searchbox__button ']");
        private readonly string childrenAgeXPath = "//select[@name='age']";


        public SearchingPageObject(IWebDriver driver)
        {
            this.driver = driver;
        }

        public SearchingPageObject SetCheckInDate()
        {
            var checkInDate = DateTime.Now.AddDays(7).ToString("yyyy-MM-dd");
            By dateBtn = By.XPath($"//div[@data-bui-ref='calendar-content']//td[@data-date='{checkInDate}']");
            Utils.WaitForElement(() => driver.FindElement(dateBtn).Displayed == true, TimeSpan.FromSeconds(5));
            driver.FindElement(dateBtn).Click();
            return this;
        }

        public int GetNumOfProperties()
        {
            string num = driver.FindElement(searchingRes).Text.Split(' ')[1];
            return int.Parse(num);
        }

        public SearchingPageObject SetCheckOutDate()
        {
            var date = DateTime.Now.AddDays(9).ToString("yyyy-MM-dd");
            By dateBtn = By.XPath($"//div[@data-bui-ref='calendar-content']//td[@data-date='{date}']");
            ClickCheckOutButton();
            driver.FindElement(dateBtn).Click();

            return this;
        }

        public SearchingPageObject SetNumOfPeopleAndRooms(uint adults, uint children, uint childrenAge, uint rooms)
        {
            driver.FindElement(people).Click();

            for (int i = 2; i < adults; i++)
            {
                ClickAddAdultsButton();
            }

            for (int i = 0; i < children; i++)
            {
                ClickAddChildrenButton();
                driver.FindElement(By.XPath(childrenAgeXPath + $"[{i+1}]")).Click();
                driver.FindElement(By.XPath(childrenAgeXPath + $"[{i + 1}]" + $"/option[{childrenAge + 2}]")).Click();
            }
            for (int i = 1; i < rooms; i++)
            {
                ClickAddRoomsButton();
            }

            ClickDoneButton();
            ClickSearchButton();
              
            return this;
        }

        public void ClickCheckOutButton()
        {
            driver.FindElement(checkOutDateBtn).Click();
        }

        public void ClickCheckInButton()
        {
            driver.FindElement(checkInDateBtn).Click();
        }

        public void ClickSearchButton()
        {
            driver.FindElement(searchButton).Click();
        }

        public void ClickDoneButton()
        {
            driver.FindElement(doneBtn).Click();
        }

        public void ClickAddAdultsButton()
        {
            driver.FindElement(increaseAdults).Click();
        }

        public void ClickAddChildrenButton()
        {
            driver.FindElement(increaseChildren).Click();
        }

        public void ClickAddRoomsButton()
        {
            driver.FindElement(increaseRooms).Click();
        }
    }
}

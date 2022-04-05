using BookingTest.PageObjects;
using NUnit.Framework;
using System.Threading;

namespace BookingTest.Tests
{
    [TestFixture]
    public class SearchingPageTest : BaseTest
    {
        private readonly uint numOfAdults = 2;
        private readonly uint numOfChildren = 1;
        private readonly uint childrenAge = 13;
        private readonly uint numOfRooms = 2;
        private readonly string place = "Vilnius";

        [Test]
        public void IsFiltersWorking()
        {
            var mainPage = new MainMenuPageObject(driver);
            var page = mainPage.Search(place).SubmitSearch();
            var numOfres = page.SetCheckInDate()
                                .SetCheckOutDate()
                                .SetNumOfPeopleAndRooms(numOfAdults, numOfChildren, childrenAge, numOfRooms)
                                .GetNumOfProperties();
            Thread.Sleep(5000);
            Assert.IsTrue(numOfres > 0);
        }
    }
}

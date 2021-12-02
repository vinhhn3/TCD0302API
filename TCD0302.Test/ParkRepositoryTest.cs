using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TCD0302API.Models;
using TCD0302API.Repositories.Interfaces;

namespace TCD0302.Test
{
  [TestFixture]
  public class ParkRepositoryTest
  {
    private IParkRepository MockParkRepository;

    [SetUp]
    public void SetUp()
    {
      List<Park> parks = new List<Park>()
      {
        new Park{ Id = 1, Name = "Park 1", State = "State 1", CreatedAt = DateTime.Now, Established = new DateTime(2022,10, 10) },
        new Park{ Id = 2, Name = "Park 2", State = "State 2", CreatedAt = DateTime.Now, Established = new DateTime(2022,10, 10) },
        new Park{ Id = 3, Name = "Park 3", State = "State 3", CreatedAt = DateTime.Now, Established = new DateTime(2022,10, 10) },
      };

      var mockParkRepository = new Mock<IParkRepository>();

      mockParkRepository.Setup(m => m.GetParks()).Returns(parks);

      mockParkRepository.Setup(m => m.GetPark(It.IsAny<int>()))
        .Returns((int i) => parks.Where(x => x.Id == i).Single());

      MockParkRepository = mockParkRepository.Object;
    }

    [Test]
    public void CanReturnAllParks()
    {
      // Arrange

      // Act
      var testParks = MockParkRepository.GetParks();

      // Assert
      Assert.IsNotNull(testParks);
      Assert.AreEqual(3, testParks.Count);
    }

    [Test]
    public void CanReturnASinglePark()
    {
      // Arrange

      // Act
      var park = MockParkRepository.GetPark(1);

      // Assert
      Assert.IsNotNull(park);
      Assert.AreEqual(1, park.Id);
      Assert.AreEqual("State 1", park.State);
      Assert.IsInstanceOf<Park>(park);
    }
  }
}

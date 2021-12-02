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

      mockParkRepository.Setup(m => m.DeletePark(It.IsAny<Park>()))
        .Returns((Park p) => parks.Remove(p));

      mockParkRepository.Setup(m => m.CreatePark(It.IsAny<Park>()))
        .Returns((Park p) =>
        {
          if (p.Id.Equals(default(int)))
          {
            p.Id = parks.Count + 1;
            parks.Add(p);
            return true;
          }
          else return false;
        });

      mockParkRepository.Setup(m => m.UpdatePark(It.IsAny<Park>()))
        .Returns((Park p) =>
        {
          var park = parks[p.Id];
          park.State = p.State;
          park.Name = p.Name;
          park.Established = p.Established;
          park.CreatedAt = p.CreatedAt;
          return true;
        });


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

    [Test]
    public void CanRemoveAPark()
    {
      // Arrange
      var itemToRemoved = MockParkRepository.GetPark(1);
      // Act
      var result = MockParkRepository.DeletePark(itemToRemoved);


      // Assert
      Assert.IsTrue(result);

      var parks = MockParkRepository.GetParks();
      Assert.AreEqual(2, parks.Count);
    }

    [Test]
    public void CanCreateAPark()
    {
      // Arrange

      // Assert
      var result = MockParkRepository.CreatePark(
        new Park
        {
          Name = "Park 4",
          State = "State 4",
          CreatedAt = DateTime.Now,
          Established = DateTime.Now
        });
      // Act
      Assert.IsTrue(result);

      var parks = MockParkRepository.GetParks();
      Assert.AreEqual(4, parks.Count);

      var park = MockParkRepository.GetPark(4);
      Assert.AreEqual("Park 4", park.Name);
    }

    [Test]
    public void CanUpdateAPark()
    {
      // Arrange
      var park = MockParkRepository.GetPark(1);
      park.Name = "Name 1 updated ...";
      park.State = "State 1 updated ...";
      // Act
      var result = MockParkRepository.UpdatePark(park);

      // Assert
      Assert.IsTrue(result);

      var parkAfterUpdate = MockParkRepository.GetPark(1);
      Assert.AreEqual("Name 1 updated ...", parkAfterUpdate.Name);
      Assert.AreEqual("State 1 updated ...", parkAfterUpdate.State);

    }
  }
}

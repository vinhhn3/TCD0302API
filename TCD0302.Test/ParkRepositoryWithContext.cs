using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TCD0302API.Data;
using TCD0302API.Models;
using TCD0302API.Repositories;
using TCD0302API.Repositories.Interfaces;

namespace TCD0302.Test
{
  [TestFixture]
  internal class ParkRepositoryWithContext
  {
    private IParkRepository _parkRepository;
    private ApplicationDbContext _context;


    [OneTimeSetUp]
    public void Setup()
    {
      // Create In Memory Database
      var options = new DbContextOptionsBuilder<ApplicationDbContext>()
        .UseInMemoryDatabase(databaseName: "DatabaseName")
        .Options;

      // Create the DbContext
      _context = new ApplicationDbContext(options);

      // Init some data for testing
      _context.Parks.Add(new Park
      {
        Id = 1,
        Name = "Name 1",
        State = "State 1",
        CreatedAt = DateTime.Now,
        Established = DateTime.Now
      });
      _context.Parks.Add(new Park
      {
        Id = 2,
        Name = "Name 2",
        State = "State 2",
        CreatedAt = DateTime.Now,
        Established = DateTime.Now
      });
      _context.Parks.Add(new Park
      {
        Id = 3,
        Name = "Name 3",
        State = "State 3",
        CreatedAt = DateTime.Now,
        Established = DateTime.Now
      });
      _context.SaveChanges();

      // Init the repository
      _parkRepository = new ParkRepository(_context);
    }

    [Test]
    public void CanReturnAllParks()
    {
      // Arrange

      // Act
      var parks = _parkRepository.GetParks();
      // Assert
      Assert.IsNotNull(parks);
      Assert.AreEqual(3, parks.Count);
    }

    [Test]
    public void CanReturnPark()
    {
      // Arrange
      var id = 2;
      // Act
      var park = _parkRepository.GetPark(id);
      // Assert
      Assert.IsNotNull(park);
      // Check the result is the type Park
      Assert.IsInstanceOf<Park>(park);
      Assert.AreEqual("Name 2", park.Name);
      Assert.AreEqual("State 2", park.State);
    }

    [Test]
    public void ParkExists_IdExistsInContext_ReturnTrue()
    {
      // Arrange

      // Act
      var result = _parkRepository.ParkExists(2);
      // Assert
      Assert.IsTrue(result);
    }

    [Test]
    public void ParkExists_IdNotExistsInContext_ReturnFalse()
    {
      // Arrange

      // Act
      var result = _parkRepository.ParkExists(10);
      // Assert
      Assert.IsFalse(result);
    }

    [Test]
    public void ParkExists_NameExistsInContext_ReturnTrue()
    {
      // Arrange
      var nameToSearch = "Name 2";
      // Act
      var result = _parkRepository.ParkExists(nameToSearch);
      // Assert
      Assert.IsTrue(result);
    }

    [Test]
    public void ParkExists_NameNotExistsInContext_ReturnFalse()
    {
      // Arrange
      var nameToSearch = "Name 10";
      // Act
      var result = _parkRepository.ParkExists(nameToSearch);
      // Assert
      Assert.IsFalse(result);
    }

    [Test]
    public void Create_CanCreateNewPark_ReturnTrue()
    {
      // Arrange
      var newPark = new Park
      {
        Name = "Name 4",
        State = "State 4",
        CreatedAt = DateTime.Now,
        Established = DateTime.Now
      };
      // Act
      var result = _parkRepository.CreatePark(newPark); // return True
      // Assert
      Assert.AreEqual(4, _context.Parks.Count());
      Assert.IsTrue(result);
    }

    [Test]
    public void UpdatePark_CanUpdateExistedPark_ReturnTrue()
    {
      // Arrange
      var parkToUpdated = new Park
      {
        Id = 3,
        Name = "Name 3 updated ...",
        State = "State 3 updated ...",
        CreatedAt = DateTime.Now,
        Established = DateTime.Now
      };
      // Act
      var result = _parkRepository.UpdatePark(parkToUpdated);
      // Assert
      Assert.IsTrue(result);

      var park = _parkRepository.GetPark(3);
      Assert.AreEqual(parkToUpdated.Name, park.Name);
      Assert.AreEqual(parkToUpdated.State, park.State);
    }

    [Test]
    public void UpdatePark_CannotUpdateNotExistedPark_ReturnFalse()
    {
      // Arrange
      var parkToUpdated = new Park
      {
        Id = 10,
        Name = "Name not existed ...",
        State = "State not existed ...",
        CreatedAt = DateTime.Now,
        Established = DateTime.Now
      };
      // Act
      var result = _parkRepository.UpdatePark(parkToUpdated);
      // Assert
      Assert.IsFalse(result);
    }

    [Test]
    public void DeletePark_CanDeleteExistedPark_ReturnTrue()
    {
      // Arrange
      var parkToDeleted = new Park
      {
        Id = 1,
        Name = "Name 1",
        State = "State 1",
        CreatedAt = DateTime.Now,
        Established = DateTime.Now
      };
      // Act
      var result = _parkRepository.DeletePark(parkToDeleted);
      // Assert
      Assert.IsTrue(result);
    }
    [Test]
    public void DeletePark_CanNotDeleteNotExistedPark_ReturnFalse()
    {
      // Arrange
      var parkToDeleted = new Park
      {
        Id = 10,
        Name = "Name 10",
        State = "State 10",
        CreatedAt = DateTime.Now,
        Established = DateTime.Now
      };
      // Act
      var result = _parkRepository.DeletePark(parkToDeleted);
      // Assert
      Assert.IsFalse(result);
    }
  }
}

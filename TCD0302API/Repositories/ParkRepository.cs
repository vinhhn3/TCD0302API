using System;
using System.Collections.Generic;
using System.Linq;
using TCD0302API.Data;
using TCD0302API.Models;
using TCD0302API.Repositories.Interfaces;

namespace TCD0302API.Repositories
{
  public class ParkRepository : IParkRepository
  {
    private readonly ApplicationDbContext _context;
    public ParkRepository(ApplicationDbContext context)
    {
      _context = context;
    }
    public bool CreatePark(Park park)
    {
      _context.Parks.Add(park);
      return Save();
    }

    public bool DeletePark(Park park)
    {
      _context.Remove(park);
      return Save();
    }

    public Park GetPark(int parkId)
    {
      return _context.Parks.FirstOrDefault(p => p.Id.Equals(parkId));
    }

    public ICollection<Park> GetParks()
    {
      return _context.Parks
        .OrderBy(p => p.Name)
        .ToList();
    }

    public bool ParkExists(int parkId)
    {
      return _context.Parks.Any(p => p.Id.Equals(parkId));
    }

    public bool ParkExists(string name)
    {
      return _context.Parks.Any(
        p => p.Name.ToLower().Trim().Equals(name.ToLower().Trim()));
    }

    private bool Save()
    {
      return _context.SaveChanges() >= 0 ? true : false;
    }

    public bool UpdatePark(Park park)
    {
      _context.Parks.Update(park);
      return Save();
    }
  }
}

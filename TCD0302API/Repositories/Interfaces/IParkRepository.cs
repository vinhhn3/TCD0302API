using System.Collections.Generic;
using TCD0302API.Models;

namespace TCD0302API.Repositories.Interfaces
{
  public interface IParkRepository
  {
    ICollection<Park> GetParks();
    Park GetPark(int parkId);
    bool ParkExists(int parkId);
    bool ParkExists(string name);
    bool CreatePark(Park park);
    bool UpdatePark(Park park);
    bool DeletePark(Park park);
  }
}

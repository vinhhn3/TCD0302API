using AutoMapper;
using TCD0302API.Dtos;
using TCD0302API.Models;

namespace TCD0302API.Mapper
{
  public class ApiMapping : Profile
  {
    public ApiMapping()
    {
      CreateMap<Park, ParkDto>().ReverseMap();
    }
  }
}

using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TCD0302API.Dtos;
using TCD0302API.Repositories.Interfaces;

namespace TCD0302API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ParksController : ControllerBase
  {
    private IParkRepository _parkRepo;
    private readonly IMapper _mapper;
    public ParksController(IParkRepository parkRepo, IMapper mapper)
    {
      _parkRepo = parkRepo;
      _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetParks()
    {
      var parks = _parkRepo.GetParks();
      var parksDto = new List<ParkDto>();
      foreach (var item in parks)
      {
        //parksDto.Add(new ParkDto
        //{
        //  Name = item.Name,
        //  State = item.State,
        //  Established = item.Established,
        //});
        parksDto.Add(_mapper.Map<ParkDto>(item));

      }
      return Ok(parksDto);
    }

  }
}

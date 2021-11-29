using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TCD0302API.Dtos;
using TCD0302API.Models;
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

    [HttpGet("{parkId:int}", Name = "GetPark")]
    public IActionResult GetPark(int parkId)
    {
      var park = _parkRepo.GetPark(parkId);
      if (park == null) return NotFound();

      var parkDto = _mapper.Map<ParkDto>(park);
      return Ok(parkDto);
    }

    [HttpPost]
    public IActionResult CreatePark([FromBody] ParkDto parkDto)
    {
      if (parkDto == null) return BadRequest(ModelState);

      if (_parkRepo.ParkExists(parkDto.Name))
      {
        ModelState.AddModelError("", "Park Name already existed ...");
        return BadRequest(ModelState);
      }

      var park = _mapper.Map<Park>(parkDto);

      if (!_parkRepo.CreatePark(park))
      {
        ModelState.AddModelError("", "Something went wrong ...");
        return BadRequest(ModelState);
      }

      return StatusCode(201);
    }

    [HttpPatch("{parkId:int}", Name = "UpdatePark")]
    public IActionResult UpdatePark(int parkId, [FromBody] ParkDto parkDto)
    {
      if (parkDto == null || parkId != parkDto.Id) return BadRequest();

      var park = _mapper.Map<Park>(parkDto);

      if (!_parkRepo.UpdatePark(park))
      {
        return BadRequest("Something went wrong ...");
      }

      return NoContent();
    }

    [HttpDelete("{parkId:int}", Name = "DeletePark")]
    public IActionResult DeletePark(int parkId)
    {
      if (!_parkRepo.ParkExists(parkId)) return NotFound();

      var park = _parkRepo.GetPark(parkId);

      if (!_parkRepo.DeletePark(park))
      {
        return BadRequest("Something went wrong ...");
      }

      return StatusCode(204);
    }
  }
}

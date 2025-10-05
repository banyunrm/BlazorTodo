using Microsoft.AspNetCore.Mvc;
using BlazorTodo.WebUI.Data;
using BlazorTodo.WebUI.Models;

namespace BlazorTodo.WebUI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CountriesController : ControllerBase
{
    private readonly AppDbContext _context;
    public CountriesController(AppDbContext context) => _context = context;

    [HttpGet]
    public IEnumerable<Country> Get() => _context.Countries.ToList();

    [HttpGet("{id}")]
    public ActionResult<Country> Get(int id)
    {
        var country = _context.Countries.Find(id);
        if (country == null) return NotFound();
        return country;
    }

    [HttpPost]
    public IActionResult Post(Country country)
    {
        _context.Countries.Add(country);
        _context.SaveChanges();
        return CreatedAtAction(nameof(Get), new { id = country.Id }, country);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, Country country)
    {
        if (id != country.Id) return BadRequest();
        _context.Countries.Update(country);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var country = _context.Countries.Find(id);
        if (country == null) return NotFound();
        _context.Countries.Remove(country);
        _context.SaveChanges();
        return NoContent();
    }
}

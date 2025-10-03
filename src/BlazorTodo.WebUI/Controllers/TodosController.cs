using Microsoft.AspNetCore.Mvc;
using BlazorTodoApp.Data;
using BlazorTodoApp.Models;

namespace BlazorTodoApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodosController : ControllerBase
{
    private readonly AppDbContext _context;
    public TodosController(AppDbContext context) => _context = context;

    [HttpGet]
    public IEnumerable<Todo> Get() => _context.Todos.ToList();

    [HttpGet("{id}")]
    public ActionResult<Todo> Get(int id)
    {
        var todo = _context.Todos.Find(id);
        if (todo == null) return NotFound();
        return todo;
    }

    [HttpPost]
    public IActionResult Post(Todo todo)
    {
        _context.Todos.Add(todo);
        _context.SaveChanges();
        return CreatedAtAction(nameof(Get), new { id = todo.Id }, todo);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, Todo todo)
    {
        if (id != todo.Id) return BadRequest();
        _context.Todos.Update(todo);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var todo = _context.Todos.Find(id);
        if (todo == null) return NotFound();
        _context.Todos.Remove(todo);
        _context.SaveChanges();
        return NoContent();
    }
}

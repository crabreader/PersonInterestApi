using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonInterestApi.Models;
using PersonInterestApi.Data;

[ApiController]
[Route("api/[controller]")]
public class PersonController : ControllerBase
{
    private readonly PersonContext _context;

    public PersonController(PersonContext context)
    {
        _context = context;
    }

    // Get all people
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Person>>> GetPeople()
    {
        return await _context.People.Include(p => p.PersonInterests)
                                    .ThenInclude(pi => pi.Interest)
                                    .ToListAsync();
    }

    // Get interests for a specific person
    [HttpGet("{id}/interests")]
    public async Task<ActionResult<IEnumerable<Interest>>> GetPersonInterests(int id)
    {
        var person = await _context.People.Include(p => p.PersonInterests)
                                          .ThenInclude(pi => pi.Interest)
                                          .FirstOrDefaultAsync(p => p.Id == id);

        if (person == null)
        {
            return NotFound();
        }

        return Ok(person.PersonInterests.Select(pi => pi.Interest));
    }

    // Get links for a specific person
    [HttpGet("{id}/links")]
    public async Task<ActionResult<IEnumerable<Link>>> GetPersonLinks(int id)
    {
        var person = await _context.People.Include(p => p.PersonInterests)
                                          .ThenInclude(pi => pi.Interest.Links)
                                          .FirstOrDefaultAsync(p => p.Id == id);

        if (person == null)
        {
            return NotFound();
        }

        var links = person.PersonInterests
                          .SelectMany(pi => pi.Interest.Links)
                          .Where(link => link.PersonId == id)
                          .ToList();

        return Ok(links);
    }

    // Add a new person
    [HttpPost]
    public async Task<ActionResult<Person>> CreatePerson([FromBody] Person person)
    {
        _context.People.Add(person);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetPeople), new { id = person.Id }, person);
    }
}

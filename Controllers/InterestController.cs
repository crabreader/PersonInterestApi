using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonInterestApi.Models;
using PersonInterestApi.Data;

[ApiController]
[Route("api/[controller]")]
public class InterestController : ControllerBase
{
    private readonly PersonContext _context;

    public InterestController(PersonContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Interest>>> GetAllInterests()
    {
        var interests = await _context.Interests.ToListAsync();
        return Ok(interests);
    }

    // Add a new interest
    [HttpPost]
    public async Task<ActionResult<Interest>> CreateInterest([FromBody] Interest interest)
    {
        _context.Interests.Add(interest);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(CreateInterest), new { id = interest.Id }, interest);
    }

    // Endpoint to add an existing interest to an existing person using their IDs
    [HttpPost("{personId}/link-interest/{interestId}")]
    public async Task<IActionResult> LinkPersonToInterest(int personId, int interestId)
    {
        // Fetch the person and interest from the database
        var person = await _context.People.FindAsync(personId);
        var interest = await _context.Interests.FindAsync(interestId);

        // If either the person or interest doesn't exist, return a 404 Not Found
        if (person == null)
        {
            return NotFound($"Person with ID {personId} not found.");
        }

        if (interest == null)
        {
            return NotFound($"Interest with ID {interestId} not found.");
        }

        // Check if the relationship already exists
        var existingLink = await _context.PersonInterests
            .FirstOrDefaultAsync(pi => pi.PersonId == personId && pi.InterestId == interestId);

        if (existingLink != null)
        {
            return Conflict($"Person with ID {personId} is already linked to interest with ID {interestId}.");
        }

        // Create the relationship
        var personInterest = new PersonInterest
        {
            PersonId = personId,
            InterestId = interestId
        };

        _context.PersonInterests.Add(personInterest);
        await _context.SaveChangesAsync();

        return Ok($"Person with ID {personId} successfully linked to interest with ID {interestId}.");
    }

    // Endpoint to add a link to a specific person and interest
    [HttpPost("{personId}/interest/{interestId}/link")]
    public async Task<IActionResult> AddLink(int personId, int interestId, [FromBody] Link link)
    {
        // Validate the person and interest IDs
        var person = await _context.People.FindAsync(personId);
        var interest = await _context.Interests.FindAsync(interestId);

        if (person == null || interest == null)
        {
            return NotFound("Person or Interest not found.");
        }

        // Set the foreign keys
        link.PersonId = personId;
        link.InterestId = interestId;

        // Add the link to the database
        _context.Links.Add(link);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(AddLink), new { id = link.Id }, link); // Respond with the created link
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SdgStaffDirectory.Models;


namespace staffapi.Controllers
{
  [Route("api/{company}/[controller]")]
  [ApiController]
  public class EmployeesController : ControllerBase
  {
    private readonly DatabaseContext _context;

    public EmployeesController(DatabaseContext context)
    {
      _context = context;
    }

    // GET: api/Person
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Object>>> GetEmployees(string company)
    {
      return await _context.Employees.Where(w => w.CompanyKey == company).Select(s => new
      {
        s.FirstName,
        s.LastName,
        s.Id,
        s.IsFullTime,
        s.JobTitle,
        s.ProfileImage
      }).ToListAsync();
    }

    [HttpGet("/api/Employees")]
    public async Task<ActionResult<IEnumerable<Object>>> GetAllEmployees()
    {
      return await _context.Employees.Select(s => new
      {
        Name = s.FirstName + " " + s.LastName,
        s.Id,
        s.CompanyKey
      }).ToListAsync();
    }

    // GET: api/Person/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Person>> GetPerson(int id, string company)
    {
      var person = await _context.Employees.FirstOrDefaultAsync(f => f.Id == id && f.CompanyKey == company);

      if (person == null)
      {
        return NotFound();
      }

      return person;
    }

    // PUT: api/Person/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutPerson([FromRoute]int id, [FromBody] Person person, [FromRoute] string company)
    {

      person.CompanyKey = company;
      if (id != person.Id)
      {
        return BadRequest();
      }

      _context.Entry(person).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!PersonExists(id))
        {
          return NotFound();
        }
        else
        {
          throw;
        }
      }

      return Ok(person);
    }

    // POST: api/Person
    [HttpPost]
    public async Task<ActionResult<Person>> PostPerson([FromBody]Person person, [FromRoute] string company)
    {
      person.CompanyKey = company;
      _context.Employees.Add(person);
      await _context.SaveChangesAsync();

      return Ok(person);
    }

    // DELETE: api/Person/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<Person>> DeletePerson(int id, string company)
    {
      var person = await _context.Employees.FirstOrDefaultAsync(f => f.Id == id && f.CompanyKey == company);
      if (person == null)
      {
        return NotFound();
      }

      _context.Employees.Remove(person);
      await _context.SaveChangesAsync();

      return Ok(person);
    }

    private bool PersonExists(int id)
    {
      return _context.Employees.Any(e => e.Id == id);
    }
  }
}

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
  [Route("api/[controller]")]
  [ApiController]
  public class CompanyController : ControllerBase
  {
    private readonly DatabaseContext _context;

    public CompanyController(DatabaseContext context)
    {
      _context = context;
    }

    // GET: api/Company
    [HttpGet]
    public async Task<ActionResult<IEnumerable<object>>> GetAllCompanies()
    {
      return await _context.Employees.GroupBy(person => person.CompanyKey, value => value).Select(s => new { name = s.Key, numberOfEmployees = s.Count() }).ToListAsync();
    }

    // GET: api/Company
    [HttpGet("{company}")]
    public async Task<ActionResult<IEnumerable<object>>> GetAllCompanies(string company)
    {
      return await _context.Employees.Where(w => w.CompanyKey == company).ToListAsync();
    }

    [HttpDelete]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<ActionResult<object>> DeleteAll()
    {
      var employees = _context.Employees;
      _context.Employees.RemoveRange(employees);
      await _context.SaveChangesAsync();
      return Ok();
    }

    // DELETE: api/Company/fakebook
    [HttpDelete("{companyKey}")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<ActionResult<object>> DeletePerson(string companyKey)
    {
      // removes all employees for that company
      var employees = _context.Employees.Where(w => w.CompanyKey == companyKey);
      var count = employees.Count();
      if (count == 0)
      {
        return NotFound();
      }
      _context.Employees.RemoveRange(employees);
      await _context.SaveChangesAsync();

      return new { company = companyKey, numberEmployeesDelete = count };
    }

  }
}

using System;

namespace SdgStaffDirectory.Models
{
  public class Person
  {
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public DateTime? Birthday { get; set; }
    public DateTime? HiredDate { get; set; }
    public bool IsFullTime { get; set; }

    public string ProfileImage { get; set; }
    public string JobTitle { get; set; }

    public string JobDescription { get; set; }

    public string PhoneNumber { get; set; }

    public string InterestingFact { get; set; }

    public string Address { get; set; }
    public string City { get; set; }
    public string Zip { get; set; }
    public string State { get; set; }
    public double Salary { get; set; }

    public string Gender { get; set; }

    public string Email { get; set; }

    public string EmergencyContactPerson { get; set; }
    public string EmergencyContactPhone { get; set; }
    public string EmergencyContactAddress { get; set; }

    public int PtoHours { get; set; } = 40;

    public string CompanyKey { get; set; }

  }
}
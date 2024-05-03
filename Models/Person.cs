using System.Text.Json.Serialization;

namespace PersonInterestApi.Models;

public class Person
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string PhoneNumber { get; set; }

    [JsonIgnore] // Ignore this during serialization
    public ICollection<PersonInterest> PersonInterests { get; set; } = new List<PersonInterest>();
}
using System.Text.Json.Serialization;

namespace PersonInterestApi.Models;

public class Interest
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }

    [JsonIgnore] // Ignore this during serialization
    public ICollection<PersonInterest> PersonInterests { get; set; } = new List<PersonInterest>();
    public ICollection<Link> Links { get; set; } = new List<Link>();
}
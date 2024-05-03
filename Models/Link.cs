namespace PersonInterestApi.Models;

public class Link
{
    public int Id { get; set; }
    public string URL { get; set; }
    public int PersonId { get; set; }
    public int InterestId { get; set; }

    // public Person Person { get; set; }
    // public Interest Interest { get; set; }
}
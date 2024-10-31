namespace SIT.Core.Domain.ValueObjects;

public record Location
{
    public string ZipCode { get; set; }
    public string City { get; set; }
    public string Region { get; set; }
    public string Country { get; set; }
    
    private Location(string zipCode, string city, string region, string country)
    {
        ZipCode = zipCode;
        City = city;
        Region = region;
        Country = country;
    }

    public static Location Create(string zipCode, string city, string region, string country)
    {
        return new Location(zipCode, city, region, country);
    }
}
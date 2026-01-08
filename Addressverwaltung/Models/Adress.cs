using SQLite;

namespace Adressverwaltung.Models;

public class Address
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Company { get; set; }

    public string Street { get; set; }
    public string ZipCode { get; set; }
    public string City { get; set; }

    public string Phone { get; set; }
    public string PhotoPath { get; set; }
}

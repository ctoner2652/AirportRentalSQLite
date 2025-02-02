using System.ComponentModel.DataAnnotations;

namespace AirportLockerRental.UI.DataStorage;

public class Rental
{
    [Key]
    public int LockerNumber { get; set; }
    public int UserID { get; set; }
    public string Contents { get; set; } = string.Empty;
    public string StartDate { get; set; } = string.Empty;

    public User User { get; set; } = new();
}
public class User
{
    [Key]
    public int UserID { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string Salt  { get; set; } = string.Empty;

    public List<Rental> Rentals { get; set; } = new();
    public List<RentalHistory> RentalHistories { get; set; } = new();
}
public class RentalHistory
{
    [Key]
    public int RentalID { get; set; }
    public int UserID { get; set; }
    public int LockerNumber { get; set; }
    public string Contents { get; set; } = string.Empty;
    public string StartDate { get; set; } = string.Empty;
    public string EndDate { get; set; } = string.Empty;

    public User User { get; set; } = new();
}

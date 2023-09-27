namespace Mc2.CrudTest.AcceptanceTests.Customers;

public class CustomerTable
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string BankAccountNumber { get; set; }
    public bool IsDeleted { get; set; }
}


namespace Projekat_A.Models;

public partial class User
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string EmailAddress { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string AccountType { get; set; } = null!;

    public string Font { get; set; } = null!;

    public sbyte Theme { get; set; }

    public sbyte Mode { get; set; }

    public sbyte Logged { get; set; }

    public virtual Group? Group { get; set; }

    public virtual Member? Member { get; set; }

    public virtual Trainer? Trainer { get; set; }
}

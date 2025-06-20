
namespace Projekat_A.Models;

public partial class MembershipType
{
    public int IdType { get; set; }

    public string Name { get; set; } = null!;

    public decimal CurrentPrice { get; set; }

    public virtual ICollection<Membership> Memberships { get; set; } = new List<Membership>();
}



namespace Projekat_A.Models;

public partial class Membership
{
    public int IdMembership { get; set; }

    public DateOnly ExpirationDate { get; set; }

    public int MemberUserId { get; set; }

    public int MembershipTypeIdType { get; set; }

    public virtual Member MemberUser { get; set; } = null!;

    public virtual MembershipType MembershipTypeIdTypeNavigation { get; set; } = null!;

    public virtual ICollection<MembershipPayment> Membershippayments { get; set; } = new List<MembershipPayment>();

    public virtual ICollection<Promotion> Promotions { get; set; } = new List<Promotion>();
}



namespace Projekat_A.Models;
public partial class Promotion
{
    public int IdPromotion { get; set; }

    public string Description { get; set; } = null!;

    public DateTime ValidFrom { get; set; }

    public DateTime ValidUntil { get; set; }

    public decimal Discount { get; set; }

    public int MemberUserId { get; set; }

    public int MembershipIdMembership { get; set; }

    public virtual Member MemberUser { get; set; } = null!;

    public virtual Membership MembershipIdMembershipNavigation { get; set; } = null!;
}
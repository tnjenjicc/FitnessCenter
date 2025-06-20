
namespace Projekat_A.Models;

public partial class MembershipPayment
{
    public int IdPayment { get; set; }

    public DateOnly BillingDate { get; set; }

    public string PaymentMethod { get; set; } = null!;

    public decimal Price { get; set; }

    public int MembershipIdMembership { get; set; }

    public bool IsPaid { get; set; }

    public virtual Membership MembershipIdMembershipNavigation { get; set; } = null!;
}



namespace Projekat_A.Models;

public partial class Member
{
    public DateOnly DateOfBirth { get; set; }

    public DateOnly DateOfEnrollment { get; set; }

    public int UserId { get; set; }

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public virtual ICollection<GroupMembership> Groupmemberships { get; set; } = new List<GroupMembership>();

    public virtual ICollection<Membership> Memberships { get; set; } = new List<Membership>();

    public virtual ICollection<Promotion> Promotions { get; set; } = new List<Promotion>();

    public virtual ICollection<TrainingReservation> Trainingreservations { get; set; } = new List<TrainingReservation>();

    public virtual User User { get; set; } = null!;
}

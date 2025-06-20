
namespace Projekat_A.Models;

public partial class Group
{
    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int MaxNumberOfMembers { get; set; }

    public int TrainerUserId { get; set; }

    public int UserId { get; set; }

    public virtual ICollection<GroupMembership> GroupMemberships { get; set; } = new List<GroupMembership>();

    public virtual Trainer TrainerUser { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}


namespace Projekat_A.Models;

public partial class TrainingReservation
{
    public int IdReservation { get; set; }

    public int MemberUserId { get; set; }

    public int TrainingSessionIdSession { get; set; }

    public virtual Member MemberUser { get; set; } = null!;

    public virtual TrainingSession TrainingSessionIdSessionNavigation { get; set; } = null!;
}

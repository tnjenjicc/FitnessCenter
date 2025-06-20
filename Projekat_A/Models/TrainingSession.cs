
namespace Projekat_A.Models;

public partial class TrainingSession
{
    public int IdSession { get; set; }

    public string Session { get; set; } = null!;

    public int TrainerUserId { get; set; }

    public int HallIdHall { get; set; }

    public virtual Hall HallIdHallNavigation { get; set; } = null!;

    public virtual Trainer TrainerUser { get; set; } = null!;

    public virtual ICollection<TrainingReservation> Trainingreservations { get; set; } = new List<TrainingReservation>();
}

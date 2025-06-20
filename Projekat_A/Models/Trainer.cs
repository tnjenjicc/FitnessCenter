
namespace Projekat_A.Models;

public partial class Trainer
{
    public int UserId { get; set; }

    public string Specialization { get; set; } = null!;

    public string WorkingTime { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public virtual ICollection<Group> Groups { get; set; } = new List<Group>();

    public virtual ICollection<TrainingSession> Trainingsessions { get; set; } = new List<TrainingSession>();

    public virtual User User { get; set; } = null!;
}

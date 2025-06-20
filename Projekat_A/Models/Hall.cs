
namespace Projekat_A.Models;

public partial class Hall
{
    public int IdHall { get; set; }

    public string Name { get; set; } = null!;

    public string Location { get; set; } = null!;

    public int Capacity { get; set; }

    public virtual ICollection<TrainingSession> Trainingsessions { get; set; } = new List<TrainingSession>();
}

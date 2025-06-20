using Projekat_A.Commands;

namespace Projekat_A.Util
{
    public class MenuItem
    {
        public String Text { get; set; }
        public RelayCommand Command { get; set; }
        public String Icon { get; set; }
    }
}


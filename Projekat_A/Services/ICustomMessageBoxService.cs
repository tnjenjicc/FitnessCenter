using System.Windows;

namespace Projekat_A.Services
{
    public interface ICustomMessageBoxService
    {
        bool? Show(string titleKey, string messageKey, MessageBoxButton buttons = MessageBoxButton.OK);
    }
}

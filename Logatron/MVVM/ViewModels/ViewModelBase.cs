using CommunityToolkit.Mvvm.ComponentModel;

namespace Logatron.MVVM.ViewModels
{
    public class ViewModelBase : ObservableObject
    {
        public virtual void LoadState()
        {
        }

        public virtual void SaveState()
        {
        }
    }
}

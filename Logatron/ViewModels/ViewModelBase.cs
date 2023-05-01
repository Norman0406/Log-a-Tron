using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI.Dispatching;

namespace Logatron.ViewModels;

public class ViewModelBase : ObservableRecipient
{
    protected DispatcherQueue DispatcherQueue { get; } = DispatcherQueue.GetForCurrentThread();

    public virtual void LoadState()
    {
    }

    public virtual void SaveState()
    {
    }
}
